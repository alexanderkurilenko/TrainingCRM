using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Training.Portals.Utils;

namespace Training.Portals.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            CRMConnection con = new CRMConnection();
            ViewBag.accountinfo = con.RetrieveUserEntities();
            ViewBag.a = con.RetrieveUserEntities().Count;
            return View();
       
        }
    }
}