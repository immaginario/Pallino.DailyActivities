using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pallino.DailyActivities.Model
{
    public class Customer
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string VATNumber { get; set; }
        public virtual ICollection<DailyReport> DailyReports { get; set; }

        public Customer()
        {
            this.Name = string.Empty;
            this.VATNumber = string.Empty;
            this.DailyReports = new List<DailyReport>();
        }
    }
}
