using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Pallino.DailyActivities.Model.Mappings;

namespace Pallino.DailyActivities.Tests
{
    [TestFixture]
    public class SchemaTests
    {
        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            var path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                @"script.sql");

            new SchemaExport(config)
                .SetOutputFile(path)
                .Create(true, false);
        }

        [Test]
        public void CanExportSchemaForMSSql()
        {
            Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString("sgdsfgdfgfd").ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CustomerMapping>())
                .ExposeConfiguration(BuildSchema)
                .BuildConfiguration();
        }
    }
}
