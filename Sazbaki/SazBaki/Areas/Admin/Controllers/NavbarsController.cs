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
    public class NavbarsController : Controller
    {
        private SazBakiEntities db = new SazBakiEntities();

        // GET: Admin/Navbars
        public ActionResult Index()
        {
            var navbars = db.Navbars.Include(n => n.Language);
            return View(navbars.ToList());
        }

        // GET: Admin/Navbars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Navbar navbar = db.Navbars.Find(id);
            if (navbar == null)
            {
                return HttpNotFound();
            }
            return View(navbar);
        }

        // GET: Admin/Navbars/Create
        public ActionResult Create()
        {
            ViewBag.navbar_lag_id = new SelectList(db.Languages, "Id", "language1");
            return View();
        }

        // POST: Admin/Navbars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,navbar_text,navbar_lag_id")] Navbar navbar)
        {
            if (ModelState.IsValid)
            {
                db.Navbars.Add(navbar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.navbar_lag_id = new SelectList(db.Languages, "Id", "language1", navbar.navbar_lag_id);
            return View(navbar);
        }

        // GET: Admin/Navbars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Navbar navbar = db.Navbars.Find(id);
            if (navbar == null)
            {
                return HttpNotFound();
            }
            ViewBag.navbar_lag_id = new SelectList(db.Languages, "Id", "language1", navbar.navbar_lag_id);
            return View(navbar);
        }

        // POST: Admin/Navbars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,navbar_text,navbar_lag_id")] Navbar navbar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(navbar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.navbar_lag_id = new SelectList(db.Languages, "Id", "language1", navbar.navbar_lag_id);
            return View(navbar);
        }

        // GET: Admin/Navbars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Navbar navbar = db.Navbars.Find(id);
            if (navbar == null)
            {
                return HttpNotFound();
            }
            return View(navbar);
        }

        // POST: Admin/Navbars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Navbar navbar = db.Navbars.Find(id);
            db.Navbars.Remove(navbar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
