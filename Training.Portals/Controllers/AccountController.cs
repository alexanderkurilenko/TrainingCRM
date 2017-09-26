using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Training.Portals.Models;
using Training.Portals.Repositories;
using Training.Portals.Utils.Infrastructure.Providers;

namespace Training.Portals.Controllers
{
    public class AccountController : Controller
    {
        private UnitOfWork unitOfWork;

        public AccountController()
        {
            unitOfWork=new UnitOfWork();
        }
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var type = HttpContext.User.GetType();
            var iden = HttpContext.User.Identity.GetType();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogOnModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(viewModel.Login, viewModel.Password))
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Login, viewModel.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register(string id)
        {
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel viewModel)
        {

            if (ModelState.IsValid)
            {
                var anyUser = unitOfWork.Users.RetreiveAll().Any(u => u.Login.Contains(viewModel.Login));

                if (anyUser)
                {
                    ModelState.AddModelError("", "User with this address already registered.");
                    return View(viewModel);
                }

                if (ModelState.IsValid)
                {
                    CustomMembershipProvider pr = new CustomMembershipProvider();
                    var membershipUser = pr.CreateUser(viewModel.Name,
                        Crypto.HashPassword(viewModel.Password), viewModel.Login);


                    if (membershipUser != null)
                    {
                        FormsAuthentication.SetAuthCookie(viewModel.Login, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error registration.");
                    }
                }
            }
            return View(viewModel);
        }
    }
}