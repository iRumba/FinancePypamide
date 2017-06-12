using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FinancePypamide
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //Session.Timeout = 60; 
        }

        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    //Request.Url
        //    //Response.Redirect()
        //}
    }
}
