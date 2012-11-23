using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pallino.DailyActivities.WebApp.ViewModels
{
    public class CreateDailyReportViewModel
    {
        [Display(Name="Data del rapportino")]
        public DateTime Date { get; set; }
    }
}