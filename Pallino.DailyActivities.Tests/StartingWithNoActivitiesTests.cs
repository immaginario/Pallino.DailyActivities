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
    public class StartingWithNoActivitiesTests
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
            var controller = new DailyReportsController(this.session);

            var result = controller.Index();

            var viewResult = result as ViewResult;
            var model = viewResult.Model as IEnumerable<DailyReportListItemViewModel>;

            model.Should().Not.Be.Null();
            model.Should().Have.Count.EqualTo(0);
        }

    }
}
