using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using Pallino.DailyActivities.Model.Mappings;

namespace Pallino.DailyActivities.WebApp.Helpers
{
    public class NHHelper
    {
        public static ISessionFactory BuildSessionFactory()
        {
            var connString = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            var config = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connString.ConnectionString).ShowSql())
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DailyReportMapping>())
                    .BuildConfiguration();

            var sessFactory = config.BuildSessionFactory();
            return sessFactory;
        }
    }
}