using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SazBaki.Models;

namespace SazBaki.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        SazBakiEntities db = new SazBakiEntities();
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public static Admintb log_admin;
        [HttpPost]
        public ActionResult LogInadmin(string email, string password)
        {
            log_admin = db.Admintbs.Where(s => s.admin_email == email && s.admin_password == password).FirstOrDefault();

            if (log_admin != null)
            {
                Session["login"] = log_admin.admin_email;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult LogOut()
        {
            Session["login"] = null;
            log_admin = null;
            return RedirectToAction("Index","Login");
        }
    }
}