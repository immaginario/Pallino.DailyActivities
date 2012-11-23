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
    public class CustomersController : Controller
    {
        ISession session;
        public CustomersController(ISession session)
        {
            this.session = session;
        }

        public ActionResult Index()
        {
            var customers = this.session.QueryOver<Customer>().List();
            return View(customers);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateCustomerViewModel newCustomer)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer { Name = newCustomer.Name, VATNumber = newCustomer.VATNumber };
                this.session.Save(customer);
                return RedirectToAction("Index");
            }
            return View(newCustomer);
        }

    }
}
