using FinancePypamide.App_Start;
using FinancePypamide.Models;
using FinancePypamide.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
//using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;
using Microsoft.Owin.Security;

using System.Web;
using Microsoft.AspNet.Identity;
using FinancePypamide.Diagram;

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
        public async Task<ActionResult> Signup(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var referer = Session["ref"] as string ?? model.Referer;
            
            var res = await AppUserManager.CreateUser(model.Login, model.Email, model.Password, referer);
            if (!res.Success)
            {
                AddErrors(res.Errors);
                return View(model);
            }

            return RedirectToAction(nameof(HomeController.Index), ControllerHelper.GetControllerName(nameof(HomeController)));
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Signin(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var res = await AppUserManager.GetUser(model.Login, model.Password);
            var ident = await new UserManager<User>().CreateIdentityAsync(res, DefaultAuthenticationTypes.ApplicationCookie);
            HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = true }, res)
            var returnUrl = "";
            if (res != null)
            {
                AppUser = res;
                
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(HomeController.Index), ControllerHelper.GetControllerName(nameof(HomeController)));
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
