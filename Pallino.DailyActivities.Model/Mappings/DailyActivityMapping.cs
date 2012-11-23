using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace Pallino.DailyActivities.Model.Mappings
{
    public class DailyActivityMapping: ClassMap<DailyActivity>
    {
        public DailyActivityMapping()
        {
            Table("DailyActivities");
            Id(x => x.Id);
            Map(x => x.Date).Not.Nullable();
        }
    }
}
