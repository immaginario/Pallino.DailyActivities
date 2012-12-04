using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pallino.DailyActivities.WebApp.ViewModels
{
    public class DailyReportViewModel
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public string MorningStart { get; set; }
        public string MorningEnd { get; set; }
        public string AfternoonStart { get; set; }
        public string AfternoonEnd { get; set; }
        public bool Offsite { get; set; }
        public string Notes { get; set; }

        public DailyReportViewModel()
        {
            this.Date = DateTime.Today;
        }
    }
}