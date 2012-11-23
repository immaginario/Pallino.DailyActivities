using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace Pallino.DailyActivities.Model.Mappings
{
    public class CustomerMapping: ClassMap<Customer>
    {
        public CustomerMapping()
        {
            Table("Customers");
            Id(x => x.Id);
            Map(x => x.Name).Not.Nullable().Length(255);
            Map(x => x.VATNumber).Not.Nullable().Length(13);
        }
    }
}
