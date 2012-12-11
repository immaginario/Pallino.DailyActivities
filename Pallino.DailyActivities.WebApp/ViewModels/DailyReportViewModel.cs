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
        [Required]
        public DateTime Date { get; set; }
        [Display(Name="Cliente")]
        public int CustomerId { get; set; }
        [RegularExpression(@"^[0-9]{2}:[0-9]{2}$")]
        public string MorningStart { get; set; }
        [RegularExpression(@"^[0-9]{2}:[0-9]{2}$")]
        public string MorningEnd { get; set; }
        [RegularExpression(@"^[0-9]{2}:[0-9]{2}$")]
        public string AfternoonStart { get; set; }
        [RegularExpression(@"^[0-9]{2}:[0-9]{2}$")]  
        public string AfternoonEnd { get; set; }
        public bool Offsite { get; set; }
        public string Notes { get; set; }

        public DailyReportViewModel()
        {
            this.Date = DateTime.Today;
            this.MorningStart = string.Empty;
            this.MorningEnd = string.Empty;
            this.AfternoonStart = string.Empty;
            this.AfternoonEnd = string.Empty;
            this.Notes = string.Empty;
        }
    }
}