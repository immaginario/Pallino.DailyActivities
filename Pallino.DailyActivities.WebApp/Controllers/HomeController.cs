﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pallino.DailyActivities.WebApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ModelState.AddModelError("Name", "Il nome fa pena.");

            return View();
        }

    }
}
