using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
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
        public ActionResult Create(CreateOrEditCustomerViewModel newCustomer)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer { Name = newCustomer.Name, VATNumber = newCustomer.VATNumber };
                this.session.Save(customer);
                return RedirectToAction("Index");
            }
            return View(newCustomer);
        }

        public ActionResult Edit(int id)
        {
            var customer = this.session.Load<Customer>(id);
            var viewModel = Mapper.Map<Customer, CreateOrEditCustomerViewModel>(customer);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, CreateOrEditCustomerViewModel updatedCustomer)
        {
            if (ModelState.IsValid)
            {
                var customer = this.session.Load<Customer>(id);
                customer.Name = updatedCustomer.Name;
                customer.VATNumber = updatedCustomer.VATNumber;
                this.session.Update(customer);
                return RedirectToAction("Index");
            }
            return View(updatedCustomer);
        }

        public ActionResult Delete(int id)
        {
            var customer = this.session.Load<Customer>(id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection formCollection)
        {            
            var customer = this.session.Load<Customer>(id);
            this.session.Delete(customer);
            return RedirectToAction("Index");
        }

    }
}
