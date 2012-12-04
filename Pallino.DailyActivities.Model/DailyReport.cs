using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        // 

        public DailyReport()
        {
            this.Date = DateTime.Today;
            this.Notes = string.Empty;
        }
    }
}
