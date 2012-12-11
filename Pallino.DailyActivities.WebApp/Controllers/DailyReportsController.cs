using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using NHibernate;
using Pallino.DailyActivities.Model;
using Pallino.DailyActivities.WebApp.Filters;
using Pallino.DailyActivities.WebApp.ViewModels;

namespace Pallino.DailyActivities.WebApp.Controllers
{
    [Transaction]
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
                    AfternoonEnd = dailyReportViewModel.AfternoonEnd ?? string.Empty,
                    AfternoonStart = dailyReportViewModel.AfternoonStart ?? string.Empty,
                    MorningEnd = dailyReportViewModel.MorningEnd ?? string.Empty,
                    MorningStart = dailyReportViewModel.MorningStart ?? string.Empty,
                    Notes = dailyReportViewModel.Notes ?? string.Empty,
                    Offsite = dailyReportViewModel.Offsite
                };
                this.session.Save(dailyReport);
                return RedirectToAction("ManageReport", new {id = dailyReport.Id});
            }
            return View(dailyReportViewModel);
        }

        public ActionResult Edit(int id)
        {
            var report = this.session.QueryOver<DailyReport>()
                             .Fetch(x => x.Customer).Eager
                             .Where(x => x.Id == id)
                             .SingleOrDefault();
            var customerList = this.session.QueryOver<Customer>()
                           .OrderBy(x => x.Name)
                           .Asc
                           .List();
            var selectedCustomer = customerList
                .SingleOrDefault(x => x.Id == report.Customer.Id);
            var viewModel = Mapper.Map<DailyReport, DailyReportViewModel>(report);
            ViewBag.SelectCustomerList = new SelectList(customerList, "Id", "Name", selectedCustomer);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, DailyReportViewModel model)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult ManageReport(int id)
        {
            GetDataForManageReport(id);
            return View();
        }

        void GetDataForManageReport(int id)
        {
            var report = this.session.QueryOver<DailyReport>()
                             .Fetch(x => x.Customer).Eager
                             .Fetch(x => x.Activities).Eager
                             .Where(x => x.Id == id)
                             .SingleOrDefault();
            var dailyReportViewModel = Mapper.Map<DailyReport, DailyReportViewModel>(report);
            ViewBag.Activities = report.Activities;
            ViewBag.DailyReport = dailyReportViewModel;
        }

        [HttpPost]
        public ActionResult ManageReport(int id, ActivityViewModel activityViewModel)
        {
            // validazione
            if (ModelState.IsValid)
            {
                // salvo
                var activityReport = this.session.Load<DailyReport>(id);
                activityReport.AddActivity(activityViewModel.Description, activityViewModel.Hours);
                this.session.Update(activityReport);

                return RedirectToAction("ManageReport", new { id = id });
            }
            GetDataForManageReport(id);
            return View(activityViewModel);
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
