using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using Pallino.DailyActivities.Model;
using Pallino.DailyActivities.WebApp.ViewModels;

namespace Pallino.DailyActivities.WebApp.Controllers
{
    public class DailyActivitiesController : Controller
    {
        ISession session;
        public DailyActivitiesController(ISession session)
        {
            this.session = session;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            var activities = this.session
                .QueryOver<DailyActivity>()
                .List();

            return View(activities);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateDailyActivityViewModel dailyActivity)
        {
            if (ModelState.IsValid)
            {
                var activity = new DailyActivity
                {
                    Date = dailyActivity.Date
                };
                this.session.Save(activity);
                return RedirectToAction("Index");
            }
            return View(dailyActivity);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
