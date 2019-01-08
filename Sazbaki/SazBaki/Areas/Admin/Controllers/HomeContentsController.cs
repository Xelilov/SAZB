using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SazBaki.Models;

namespace SazBaki.Areas.Admin.Controllers
{
    [AuthorizationFilterAdmin]
    public class HomeContentsController : Controller
    {
        
        private SazBakiEntities db = new SazBakiEntities();

        // GET: Admin/HomeContents
        public ActionResult Index()
        {
            var homeContents = db.HomeContents.Include(h => h.Language);
            return View(homeContents.ToList());
        }

        // GET: Admin/HomeContents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeContent homeContent = db.HomeContents.Find(id);
            if (homeContent == null)
            {
                return HttpNotFound();
            }
            return View(homeContent);
        }

        // GET: Admin/HomeContents/Create
        public ActionResult Create()
        {
            ViewBag.home_lang_id = new SelectList(db.Languages, "Id", "language1");
            return View();
        }

        // POST: Admin/HomeContents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,home_content,home_lang_id")] HomeContent homeContent)
        {
            if (ModelState.IsValid)
            {
                db.HomeContents.Add(homeContent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.home_lang_id = new SelectList(db.Languages, "Id", "language1", homeContent.home_lang_id);
            return View(homeContent);
        }

        // GET: Admin/HomeContents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeContent homeContent = db.HomeContents.Find(id);
            if (homeContent == null)
            {
                return HttpNotFound();
            }
            ViewBag.home_lang_id = new SelectList(db.Languages, "Id", "language1", homeContent.home_lang_id);
            return View(homeContent);
        }

        // POST: Admin/HomeContents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,home_content,home_lang_id")] HomeContent homeContent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(homeContent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.home_lang_id = new SelectList(db.Languages, "Id", "language1", homeContent.home_lang_id);
            return View(homeContent);
        }

        //// GET: Admin/HomeContents/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HomeContent homeContent = db.HomeContents.Find(id);
        //    if (homeContent == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(homeContent);
        //}

        //// POST: Admin/HomeContents/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    HomeContent homeContent = db.HomeContents.Find(id);
        //    db.HomeContents.Remove(homeContent);
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
