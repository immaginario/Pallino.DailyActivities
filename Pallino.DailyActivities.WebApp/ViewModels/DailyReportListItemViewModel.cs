using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pallino.DailyActivities.Model;

namespace Pallino.DailyActivities.WebApp.ViewModels
{
    public class DailyReportListItemViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public bool Offsite { get; set; }
    }
}