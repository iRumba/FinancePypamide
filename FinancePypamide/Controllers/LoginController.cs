using FinancePypamide.App_Start;
using FinancePypamide.Models;
using FinancePypamide.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
//using System.Web.Http;
using System.Web.Mvc;

namespace FinancePypamide.Controllers
{
    public class LoginController : RefererController
    {
        public ActionResult Signin()
        {
            
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Signin(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var referer = Session["ref"] as string ?? model.Referer;

            var res = await UserManager.CreateUser(model.Login, model.Email, model.Password, referer);
            if (!res.Success)
            {
                AddErrors(res.Errors);
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }



        private void AddErrors(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        //private ActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    return RedirectToAction("Index", "Home");
        //}
    }
}
