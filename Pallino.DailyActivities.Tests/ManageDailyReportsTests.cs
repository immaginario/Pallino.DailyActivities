using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using NHibernate;
using NUnit.Framework;
using Pallino.DailyActivities.Model;
using Pallino.DailyActivities.WebApp.Controllers;
using Pallino.DailyActivities.WebApp.ViewModels;
using SharpTestsEx;

namespace Pallino.DailyActivities.Tests
{
    [TestFixture]
    public class ManageDailyReportsTests
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
        public void AccessingTheReportsManagment_ShowsTheListOfReports()
        {
            var controller = new DailyReportsController(this.session);

            Mapper.CreateMap<DailyReport, DailyReportListItemViewModel>();

            var result = controller.Index();

            var viewResult = (ViewResult)result;
            
            var model = viewResult.Model as IEnumerable<DailyReportListItemViewModel>;
            
            model.Should().Not.Be.Null();
        }

        [Test]
        public void TheCreateReportView_ShowsAComboOfCustomers()
        {
            var controller = new DailyReportsController(this.session);
            this.session.Save(new Customer { Name = "Pippo1", VATNumber = "12345678901" });
            this.session.Save(new Customer { Name = "Pippo2", VATNumber = "12345678902" });
            this.session.Save(new Customer { Name = "Pippo3", VATNumber = "12345678903" });

            var result = controller.Create();

            var viewResult = (ViewResult)result;
            var selectCustomerList
                = viewResult.ViewBag.SelectCustomerList as SelectList;
            selectCustomerList.Should().Not.Be.Null();
            selectCustomerList.Count().Should().Be.EqualTo(3);
        }

        [Test]
        public void CreatingANewReport_AddsItToTheDb()
        {
            var controller = new DailyReportsController(this.session);
            string customerName = "Pippo1";
            this.session.Save(new Customer { Name = customerName, VATNumber = "12345678901" });
            var reportDate = new DateTime(2012, 11, 29, 8, 0, 0);
            var viewModel = new DailyReportViewModel
                {
                    CustomerId = 1,
                    Date = reportDate,
                    MorningStart = "09:00",
                    MorningEnd = "13:00",
                    AfternoonStart = "14:00",
                    AfternoonEnd = "18:00",
                    Offsite = true,
                    Notes = "La macchinetta del caffé fa pena."
                };

            var result = controller.Create(viewModel);

            var redirectResult = result as RedirectToRouteResult;
            var action = redirectResult.RouteValues["action"];

            var reportOnDb = this.session.Get<DailyReport>(1);
            reportOnDb.Should().Not.Be.Null();
            reportOnDb.Customer.Should().Not.Be.Null();
            reportOnDb.Customer.Name.Should().Be.EqualTo(customerName);
            reportOnDb.Date.Should().Be.EqualTo(reportDate);
        }
    }
}
