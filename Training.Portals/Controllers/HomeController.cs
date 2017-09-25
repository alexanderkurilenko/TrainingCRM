using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Xrm.Sdk;
using Training.Portals.Models;
using Training.Portals.Utils;

namespace Training.Portals.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            CRMConnection con =new CRMConnection();
            ViewBag.accountinfo=con.RetrieveEntities();
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

            CRMConnection objDAL = new CRMConnection();

            List<EntityReference> refUsers = objDAL.GetEntityReference();

            AccountEntityModels objAccountModel = new AccountEntityModels();

            Guid accountId = Guid.Empty;

            if (id != null)

            {

                accountId = Guid.Parse(id);

            }

            if (accountId != Guid.Empty)

            {

                objAccountModel = objDAL.GetCurrentRecord(accountId);

            }


            if (refUsers.Count > 0)

            {

                ViewBag.EntityReferenceUsers = new SelectList(refUsers, "Id", "Name");

            }

            return View(objAccountModel);

        }

        [HttpPost]

        public ActionResult AddNew(AccountEntityModels accountdmodel)

        {

            CRMConnection objDAL = new CRMConnection();

            Guid id = accountdmodel.AccountID;

            objDAL.SaveAccount(accountdmodel);

            return Redirect("~/Home");

            return View(accountdmodel);

        }

        public ActionResult Delete(Guid id)
        {
            CRMConnection objDAL = new CRMConnection();
            objDAL.DeleteRecord(id);
            return Redirect("~/Home");
        }


    }
}