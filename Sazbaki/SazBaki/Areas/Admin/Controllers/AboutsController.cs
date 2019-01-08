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
    public class AboutsController : Controller
    {
        private SazBakiEntities db = new SazBakiEntities();

        // GET: Admin/Abouts
        public ActionResult Index()
        {
            var abouts = db.Abouts.Include(a => a.Language);
            return View(abouts.ToList());
        }

        // GET: Admin/Abouts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.Abouts.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            return View(about);
        }

        // GET: Admin/Abouts/Create
        //public ActionResult Create()
        //{
        //    ViewBag.about_lang_id = new SelectList(db.Languages, "Id", "language1");
        //    return View();
        //}

        //// POST: Admin/Abouts/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,about_text,about_lang_id")] About about, HttpPostedFileBase about_img)
        //{
        //    if (about_img!=null)
        //    {
        //        Random rnd = new Random();
        //        var randonnumber = rnd.Next(10000, 99999);
        //        var InputFileName = Path.GetFileName(about_img.FileName);
        //        var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + randonnumber + InputFileName);
        //        //Save file to server folder
        //        about_img.SaveAs(ServerSavePath);
        //        if (ModelState.IsValid)
        //        {
        //            about.about_img = randonnumber + InputFileName;
        //            db.Abouts.Add(about);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    else
        //    {
        //        about.about_img = null;
        //        db.Abouts.Add(about);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.about_lang_id = new SelectList(db.Languages, "Id", "language1", about.about_lang_id);
        //    return View(about);
        //}

        // GET: Admin/Abouts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            About about = db.Abouts.Find(id);
            if (about == null)
            {
                return HttpNotFound();
            }
            ViewBag.about_lang_id = new SelectList(db.Languages, "Id", "language1", about.about_lang_id);
            return View(about);
        }

        // POST: Admin/Abouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string about_text, int about_lang_id, HttpPostedFileBase about_img, string current_image_name)
        {
            if (ModelState.IsValid)
            {                
                if (about_img != null)
                {
                    var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + current_image_name);
                    //Save file to server folder
                    about_img.SaveAs(ServerSavePath);

                    var about = db.Abouts.Find(id);
                    about.about_img = current_image_name;
                    about.about_text = about_text;
                    about.about_lang_id = about_lang_id;
                    db.SaveChanges();
                }
                else
                {
                    var about = db.Abouts.Find(id);
                    about.about_text = about_text;
                    about.about_lang_id = about_lang_id;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.about_lang_id = new SelectList(db.Languages, "Id", "language1", about_lang_id);
            return View();
        }

        //// GET: Admin/Abouts/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    About about = db.Abouts.Find(id);
        //    if (about == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(about);
        //}

        //// POST: Admin/Abouts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    About about = db.Abouts.Find(id);
        //    db.Abouts.Remove(about);
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
