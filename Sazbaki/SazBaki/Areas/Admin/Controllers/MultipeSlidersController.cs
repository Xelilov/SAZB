using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SazBaki.Models;

namespace SazBaki.Areas.Admin.Controllers
{
    [AuthorizationFilterAdmin]
    public class MultipeSlidersController : Controller
    {
        private SazBakiEntities db = new SazBakiEntities();

        // GET: Admin/MultipeSliders
        public ActionResult Index()
        {
            var multipeSliders = db.MultipeSliders.Include(m => m.Language);
            return View(multipeSliders.ToList());
        }

        // GET: Admin/MultipeSliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MultipeSlider multipeSlider = db.MultipeSliders.Find(id);
            if (multipeSlider == null)
            {
                return HttpNotFound();
            }
            return View(multipeSlider);
        }

        //// GET: Admin/MultipeSliders/Create
        //public ActionResult Create()
        //{
        //    ViewBag.mp_lang_id = new SelectList(db.Languages, "Id", "language1");
        //    return View();
        //}

        //// POST: Admin/MultipeSliders/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,mp_text,mp_lang_id")] MultipeSlider multipeSlider, HttpPostedFileBase imagefile, HttpPostedFileBase logo_img)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Random rnd = new Random();
        //        var randonnumber = rnd.Next(10000, 99999);
        //        var InputFileName = Path.GetFileName(imagefile.FileName);
        //        var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + randonnumber + InputFileName);
        //        //Save file to server folder
        //        imagefile.SaveAs(ServerSavePath);

        //        Random logormd = new Random();
        //        var rndmnumber = logormd.Next(10000, 99999);
        //        var FileName = Path.GetFileName(logo_img.FileName);
        //        var SavePath = Path.Combine(Server.MapPath("~/Uploads/") + rndmnumber + FileName);
        //        //Save file to server folder
        //        logo_img.SaveAs(SavePath);

        //        multipeSlider.mp_img = randonnumber + InputFileName;
        //        multipeSlider.mp_logo_img = rndmnumber + FileName;
        //        db.MultipeSliders.Add(multipeSlider);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.mp_lang_id = new SelectList(db.Languages, "Id", "language1", multipeSlider.mp_lang_id);
        //    return View(multipeSlider);
        //}

        // GET: Admin/MultipeSliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MultipeSlider multipeSlider = db.MultipeSliders.Find(id);
            if (multipeSlider == null)
            {
                return HttpNotFound();
            }
            ViewBag.mp_lang_id = new SelectList(db.Languages, "Id", "language1", multipeSlider.mp_lang_id);
            return View(multipeSlider);
        }

        // POST: Admin/MultipeSliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( int id,string mp_text,int mp_lang_id, HttpPostedFileBase imagefile, string current_image_name, HttpPostedFileBase logo_img, string current_img_name)
        {
            if (ModelState.IsValid)
            {
                if (imagefile != null)
                {
                    var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + current_image_name);
                    //Save file to server folder
                    imagefile.SaveAs(ServerSavePath);
                }

                if (logo_img != null)
                {
                    var SavePath = Path.Combine(Server.MapPath("~/Uploads/") + current_img_name);
                    //Save file to server folder
                    logo_img.SaveAs(SavePath);
                }

                var mp = db.MultipeSliders.Find(id);
                mp.mp_lang_id = mp_lang_id;
                mp.mp_text = mp_text;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.mp_lang_id = new SelectList(db.Languages, "Id", "language1", mp_lang_id);
            return View();
        }

        // GET: Admin/MultipeSliders/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    MultipeSlider multipeSlider = db.MultipeSliders.Find(id);
        //    if (multipeSlider == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(multipeSlider);
        //}

        //// POST: Admin/MultipeSliders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    MultipeSlider multipeSlider = db.MultipeSliders.Find(id);
        //    db.MultipeSliders.Remove(multipeSlider);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
