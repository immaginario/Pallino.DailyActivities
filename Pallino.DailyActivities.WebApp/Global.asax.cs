using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using NHibernate;
using Pallino.DailyActivities.Model;
using Pallino.DailyActivities.WebApp.Controllers;
using Pallino.DailyActivities.WebApp.ViewModels;

namespace Pallino.DailyActivities.WebApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            // Register ISessionFactory as Singleton 
            builder.Register<ISessionFactory>(x => Helpers.NHHelper.BuildSessionFactory())
                .SingleInstance();

            // Register ISession as instance per web request
            builder.Register<ISession>(x => x.Resolve<ISessionFactory>().OpenSession())
                .InstancePerHttpRequest();

            // Register all controllers
            builder.RegisterAssemblyTypes(typeof(CustomersController).Assembly)
                .InNamespaceOf<CustomersController>()
                .AsSelf();

            builder.RegisterFilterProvider();

            // override default dependency resolver to use Autofac
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));

            AreaRegistration.RegisterAllAreas();

            Mapper.CreateMap<Customer, CreateOrEditCustomerViewModel>();
            Mapper.CreateMap<DailyReport, DailyReportListItemViewModel>();
            Mapper.CreateMap<DailyReport, DailyReportViewModel>();
            
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
    }
}