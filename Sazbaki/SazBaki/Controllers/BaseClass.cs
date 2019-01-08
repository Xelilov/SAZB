using SazBaki.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SazBaki.Controllers
{
    public class BaseClass : Controller
    {
        int language_id = 1;
        private SazBakiEntities db = new SazBakiEntities();
       // public IndexWiewModel index= new IndexWiewModel();
        public BaseClass()
        {
            HttpCookie cookie = HttpContext.Request.Cookies["langId"];
            //HttpContext.Session["langId"] = 2;
            if (cookie != null && cookie.Value!=null)
            {
                language_id = 2/* Convert.ToInt32(HttpContext.Session["langId"]);*/;
                ViewBag._hometext = db.HomeContents.Where(s => s.home_lang_id == language_id).First();
                ViewBag._mpslider = db.MultipeSliders.Where(t => t.mp_lang_id == language_id).ToList();
            }
            else
            {
                ViewBag._hometext = db.HomeContents.Where(s => s.home_lang_id == language_id).First();
                ViewBag._mpslider = db.MultipeSliders.Where(t => t.mp_lang_id == language_id).ToList();
            }
        }

        //protected IndexWiewModel model()
        //{
        //    if (Session["langId"] != null)
        //    {
        //        language_id = Convert.ToInt32(Session["langId"]);
        //        index._hometext = db.HomeContents.Where(s => s.home_lang_id == language_id).First();
        //        index._mpslider = db.MultipeSliders.Where(t => t.mp_lang_id == language_id).ToList();
        //    }
        //    else
        //    {
        //        index._hometext = db.HomeContents.Where(s => s.home_lang_id == language_id).First();
        //        index._mpslider = db.MultipeSliders.Where(t => t.mp_lang_id == language_id).ToList();
        //    }
        //    return index;
        //}
    }
}