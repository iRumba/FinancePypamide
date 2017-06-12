using FinancePypamide.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinancePypamide.Utils
{
    public class UserController : Controller
    {
        public User AppUser
        {
            get
            {
                return Session["User"] as User;
            }
            set
            {
                Session["User"] = value;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return (Session["User"] as User) != null;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (ViewBag.IsAuthenticated = IsAuthenticated)
                ViewBag.User = AppUser;
            
        }
    }
}