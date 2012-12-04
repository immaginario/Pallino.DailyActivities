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
    public class DailyReportsController : Controller
    {
        ISession session;
        public DailyReportsController(ISession session)
        {
            this.session = session;
        }

        public ActionResult Index()
        {
            var activities = this.session
                .QueryOver<DailyReport>()
                .List();

            var vmList =
                AutoMapper.Mapper
                .Map<IEnumerable<DailyReport>, IEnumerable<DailyReportListItemViewModel>>(activities);

            //vmList = new List<DailyReportListItemViewModel> {new DailyReportListItemViewModel()};

            return View(vmList);
        }

        public ActionResult Create()
        {
            var customerList = this.session.QueryOver<Customer>()
                           .OrderBy(x => x.Name)
                           .Asc
                           .List();
            ViewBag.SelectCustomerList = new SelectList(customerList, "Id", "Name");
            var model = new DailyReportViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(DailyReportViewModel dailyReportViewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = this.session.Load<Customer>(dailyReportViewModel.CustomerId);
                var dailyReport = new DailyReport
                {
                    Customer = customer,
                    Date = dailyReportViewModel.Date,
                    AfternoonEnd = dailyReportViewModel.AfternoonEnd,
                    AfternoonStart = dailyReportViewModel.AfternoonStart,
                    MorningEnd = dailyReportViewModel.MorningEnd,
                    MorningStart = dailyReportViewModel.MorningStart,
                    Notes = dailyReportViewModel.Notes,
                    Offsite = dailyReportViewModel.Offsite
                };
                this.session.Save(dailyReport);
                return RedirectToAction("Index");
            }
            return View(dailyReportViewModel);
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
