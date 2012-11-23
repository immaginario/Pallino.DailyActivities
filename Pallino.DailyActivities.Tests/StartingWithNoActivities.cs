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
    public class StartingWithNoActivities
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
        public void EnteringTheSite_IShouldSeeAnEmptyList()
        {
            var controller = new DailyActivitiesController(this.session);

            var result = controller.Index();

            var viewResult = result as ViewResult;
            var model = viewResult.Model as IEnumerable<DailyActivity>;

            model.Should().Not.Be.Null();
            model.Should().Have.Count.EqualTo(0);
        }

        [Test]
        public void CreatingANewActivity_ShowsAListWithOneActivity()
        {
            var controller = new DailyActivitiesController(this.session);

            var dailyActivity = new CreateDailyActivityViewModel
                {
                    Date = DateTime.Today
                };
            var result = controller.Create(dailyActivity);

            var redirectResult = result as RedirectToRouteResult;
            var action = redirectResult.RouteValues["action"];

            var activityOnDb = this.session.Get<DailyActivity>(1);
            activityOnDb.Should().Not.Be.Null();
            activityOnDb.Date.Should().Be.EqualTo(DateTime.Today);
        }
    }
}
