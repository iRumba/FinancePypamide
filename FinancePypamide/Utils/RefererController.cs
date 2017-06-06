using FinancePypamide.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinancePypamide.Utils
{
    public class RefererController : UserController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Request.QueryString.Count == 0)
                base.OnActionExecuting(filterContext);
            else
            {
                var referer = Request.QueryString["ref"];
                if (!string.IsNullOrWhiteSpace(referer) && Session["ref"] == null 
                    && !UserManager.CheckUserName((string)Session["ref"])
                    && UserManager.CheckUserName(Request.QueryString["ref"]))
                    Session["ref"] = Request.QueryString["ref"];

                var action = filterContext.ActionDescriptor.ActionName;
                filterContext.Result = RedirectToAction(action);
                //this.RouteData;
            }
            
        }
    }
}