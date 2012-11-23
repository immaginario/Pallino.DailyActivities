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

        public DailyReport()
        {
            this.Date = DateTime.Today;
        }
    }
}
