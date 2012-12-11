using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace Pallino.DailyActivities.Model.Mappings
{
    public class DailyReportMapping: ClassMap<DailyReport>
    {
        public DailyReportMapping()
        {
            Table("DailyReports");
            Id(x => x.Id);
            Map(x => x.Date).Not.Nullable();
            Map(x => x.MorningStart).Not.Nullable().Length(5);
            Map(x => x.MorningEnd).Not.Nullable().Length(5);
            Map(x => x.AfternoonStart).Not.Nullable().Length(5);
            Map(x => x.AfternoonEnd).Not.Nullable().Length(5);
            Map(x => x.Notes).Not.Nullable().Length(4000);
            Map(x => x.Offsite).Not.Nullable();
            References(x => x.Customer).Not.Nullable()
                .ForeignKey("FK_DailyReports_Customers");
            HasMany(x => x.Activities).Inverse().Cascade.All();
        }
    }
}
