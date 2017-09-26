using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Training.Portals.Models;
using Training.Portals.Repositories;
using Training.Portals.Utils;

namespace Training.Portals.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private UnitOfWork unitofwork;

        public AdminController()
        {
            unitofwork = new UnitOfWork();
        }

        // GET: Admin
        public ActionResult Index()
        {

            ViewBag.accountinfo = unitofwork.Users.RetreiveAll();
            ViewBag.a = unitofwork.Users.RetreiveAll().First();
            return View();
       
        }

        public ActionResult AddNew(string id)

        {  
            User objUserModel = new User();

            Guid userId = Guid.Empty;

            if (id != null)

            {

                userId = Guid.Parse(id);

            }

            if (userId != Guid.Empty)

            {

                objUserModel = unitofwork.Users.Get(userId);

            }


            return View(objUserModel);

        }
        [HttpPost]
        public ActionResult AddNew(User usermodel)

        {
            Guid id = usermodel.UserId;

            unitofwork.Users.Update(usermodel);

            return Redirect("~/Admin");
        }
        public ActionResult Delete(Guid id)
        {
            unitofwork.Users.Delete(id);
            return Redirect("~/Admin");
        }
    }
}