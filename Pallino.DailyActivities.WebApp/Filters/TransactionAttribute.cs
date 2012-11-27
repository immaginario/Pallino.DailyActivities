using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;

namespace Pallino.DailyActivities.WebApp.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class TransactionAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public ISession Session { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentTransaction = Session.Transaction;
            currentTransaction.Begin();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var currentTransaction = Session.Transaction;
            if (currentTransaction.IsActive)
            {
                if (filterContext.Exception == null)
                {
                    try
                    {
                        currentTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        currentTransaction.Rollback();
                        throw;
                    }
                }
                else
                {
                    currentTransaction.Rollback();
                }
            }
        }
    }
}