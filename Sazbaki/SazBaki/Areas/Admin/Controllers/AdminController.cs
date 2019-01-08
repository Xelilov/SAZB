using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SazBaki.Models;

namespace SazBaki.Areas.Admin.Controllers
{
    [AuthorizationFilterAdmin]
    public class AdminController : Controller
    {
        SazBakiEntities db = new SazBakiEntities();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewPassword(string password1, string password2)
        {
            if (password1== password2)
            {
                var admin = db.Admintbs.FirstOrDefault();
                admin.admin_password = password1;
                db.SaveChanges();
                TempData["pass"] = "Sifreniz ugurla deyisdirildi";                
            }
            else
            {
                TempData["pass"] = "Sifreler eyni deyil";
            }
            return RedirectToAction("Index");
        }
    }
}