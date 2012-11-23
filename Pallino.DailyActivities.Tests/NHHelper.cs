using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Pallino.DailyActivities.Model.Mappings;

namespace Pallino.DailyActivities.Tests
{
    public class NHHelper
    {

        private static readonly FluentConfiguration Config = BuildConfiguration();

        private static readonly ISessionFactory Factory = BuildConfiguration()
                .BuildSessionFactory();

        public static FluentConfiguration BuildConfiguration()
        {
            return Fluently.Configure()
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DailyActivityMapping>())
                .Database(SQLiteConfiguration.Standard.InMemory());
        }

        public static ISession GetInMemorySession()
        {
            var session = Factory.OpenSession();
            new SchemaExport(Config.BuildConfiguration())
                .Execute(false, true, false, session.Connection, null);
            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            return session;
        }
    }
}
