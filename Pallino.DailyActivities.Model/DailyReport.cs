using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Remotion.Linq.Utilities;

namespace Pallino.DailyActivities.Model
{
    public class DailyReport
    {
        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual string MorningStart { get; set; }
        public virtual string MorningEnd { get; set; }
        public virtual string AfternoonStart { get; set; }
        public virtual string AfternoonEnd { get; set; }
        public virtual bool Offsite { get; set; }
        public virtual string Notes { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        // 

        public DailyReport()
        {
            this.Date = DateTime.Today;
            this.Notes = string.Empty;
            Activities = new List<Activity>();
        }

        public virtual void AddActivity(string description, decimal hours)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("description should not be empty");
            if (hours <= 0)
                throw new ArgumentException("hours should be positive number");
            var activity = new Activity
                {
                    DailyReport = this,
                    Hours = hours,
                    Description = description
                };
            this.Activities.Add(activity);
        }
    }
}
