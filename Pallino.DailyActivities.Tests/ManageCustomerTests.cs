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
        }

        [TearDown]
        public void TearDown()
        {
            if (this.session != null && session.IsOpen)
                this.session.Dispose();
        }

        [Test]
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

            var viewModel = new CreateCustomerViewModel { Name = "Pippo", VATNumber="12345678901" };
            
            var result = controller.Create(viewModel);

            var redirectResult = result as RedirectToRouteResult;
            var action = redirectResult.RouteValues["action"];

            var reportOnDb = this.session.Get<Customer>(1);
            reportOnDb.Should().Not.Be.Null();
        }
    }
}
