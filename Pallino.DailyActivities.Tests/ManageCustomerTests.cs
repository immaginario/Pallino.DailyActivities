using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using NHibernate;
using NUnit.Framework;
using Pallino.DailyActivities.Model;
using Pallino.DailyActivities.WebApp.Controllers;
using Pallino.DailyActivities.WebApp.ViewModels;
using SharpTestsEx;

namespace Pallino.DailyActivities.Tests
{
    [TestFixture]
    public class ManageCustomerTests
    {
        ISession session;

        [SetUp]
        public void Setup()
        {
            this.session = NHHelper.GetInMemorySession();
            this.session.Transaction.Begin();
        }

        [TearDown]
        public void TearDown()
        {
            if (this.session != null && session.IsOpen)
            {
                this.session.Transaction.Commit();
                this.session.Transaction.Dispose();
                this.session.Dispose();
            }
        }

        [Test]
        [Description("Accessing The CustomerManagment Shows The List Of Customers")]
        public void AccessingTheCustomerManagment_ShowsTheListOfCustomers()
        {
            var controller = new CustomersController(this.session);

            var result = controller.Index();

            var viewResult = (ViewResult)result;
            var model = viewResult.Model as IEnumerable<Customer>;
            model.Should().Not.Be.Null();
        }

        [Test]
        public void CreatingANewCustomer_AddsItToTheDb()
        {
            var controller = new CustomersController(this.session);

            var viewModel = new CreateOrEditCustomerViewModel { Name = "Pippo", VATNumber="12345678901" };
            
            var result = controller.Create(viewModel);

            var redirectResult = result as RedirectToRouteResult;
            var action = redirectResult.RouteValues["action"];

            var reportOnDb = this.session.Get<Customer>(1);
            reportOnDb.Should().Not.Be.Null();
        }


        [Test]
        [Description("Creating a customer with a vat number already present on the db is not allowed")]
        public void CreatingACustomerTwoTimes_IsNotAllowed()
        {
            this.session.Save(new Customer { Name = "Pippo", VATNumber = "12345678901" });
            var controller = new CustomersController(this.session);
            var viewModel = new CreateOrEditCustomerViewModel { Name = "Pippo", VATNumber = "12345678901" };

            var result = controller.Create(viewModel);

            var viewResult = result as ViewResult;
            viewResult.Should().Not.Be.Null();
            var error = viewResult.ViewData.ModelState["VATNumber"].Errors[0];
            Assert.AreEqual("Un cliente con stessa partita Iva è già presente.", error.ErrorMessage);
        }

        [Test]
        public void UpdatingACustomer_ChangesItsDataOnDb()
        {
            this.session.Save(new Customer { Name = "Pippo", VATNumber = "12345678901" });
            var controller = new CustomersController(this.session);
            var newName = "Mario";
            var newVat = "12345678902";
            var updatedCustomer = new CreateOrEditCustomerViewModel {Name = newName, VATNumber = newVat};

            var result = controller.Edit(1,updatedCustomer);

            var customerOnDb = this.session.Get<Customer>(1);
            customerOnDb.Should().Not.Be.Null();
            customerOnDb.Name.Should().Be.EqualTo(newName);
            customerOnDb.VATNumber.Should().Be.EqualTo(newVat);
        }


        [Test]
        public void DeletingACustomer_RemovesItFromDb()
        {
            this.session.Save(new Customer { Name = "Pippo", VATNumber = "12345678901" });
            var controller = new CustomersController(this.session);

            var result = controller.Delete(1, new FormCollection());

            //var customerOnDb = this.session.Get<Customer>(1);
            //customerOnDb.Should().Be.Null();

            var customers = this.session.QueryOver<Customer>().List();
            customers.Should().Have.Count.EqualTo(0);
        }
    }
}
