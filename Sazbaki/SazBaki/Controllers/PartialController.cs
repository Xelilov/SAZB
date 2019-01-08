using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SazBaki.Models;

namespace SazBaki.Controllers
{
    public class PartialController : Controller
    {
        SazBakiEntities db = new SazBakiEntities();
        // GET: Partial
        public PartialViewResult Slider()
        {
            var parial = new PartialModel();
            parial._backslider = db.BackSliders.ToList();
            return PartialView(parial);
        }

        int language_id = 1;
        public PartialViewResult Navbar()
        {
            if (Session["langId"] != null)
            {
                language_id = Convert.ToInt32(Session["langId"]);
                IndexWiewModel index = new IndexWiewModel();
                var navbar = new PartialModel();
                navbar._navbar = db.Navbars.Where(s => s.navbar_lag_id == language_id).ToList();
                return PartialView(navbar);
            }
            else
            {
                var navbar = new PartialModel();
                navbar._navbar = db.Navbars.Where(s=>s.navbar_lag_id==language_id).ToList();
                return PartialView(navbar);
            }
            
        }
    }
}