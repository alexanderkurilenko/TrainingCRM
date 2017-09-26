using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Xrm.Sdk;
using Training.Portals.Models;
using Training.Portals.Repositories;
using Training.Portals.Utils;

namespace Training.Portals.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UnitOfWork unitofwork;

        public HomeController()
        {
            unitofwork=new UnitOfWork();          
        }

        public ActionResult Index()
        {
         
            ViewBag.accountinfo=unitofwork.Accounts.RetreiveAll();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AddNew(string id)

        {
            List<EntityReference> refUsers = unitofwork.Accounts.GetEntityReference();

            Account objAccountModel = new Account();

            Guid accountId = Guid.Empty;

            if (id != null)

            {

                accountId = Guid.Parse(id);

            }

            if (accountId != Guid.Empty)

            {

                objAccountModel = unitofwork.Accounts.Get(accountId);

            }


            if (refUsers.Count > 0)

            {

                ViewBag.EntityReferenceUsers = new SelectList(refUsers, "Id", "Name");

            }

            return View(objAccountModel);

        }

        [HttpPost]

        public ActionResult AddNew(Account accountdmodel)

        {
            Guid id = accountdmodel.AccountID;

            unitofwork.Accounts.Update(accountdmodel);

            return Redirect("~/Home");
        }

        public ActionResult Delete(Guid id)
        {
            unitofwork.Accounts.Delete(id);
            return Redirect("~/Home");
        }


    }
}