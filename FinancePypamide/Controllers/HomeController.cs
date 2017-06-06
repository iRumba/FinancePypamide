using FinancePypamide.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinancePypamide.Controllers
{
    public class HomeController : RefererController
    {
        public ActionResult Index()
        {
            //if (HttpContext.Request.QueryString["qwe"] != null)
            return View();
        }
    }
}