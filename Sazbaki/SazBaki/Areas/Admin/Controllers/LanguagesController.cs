﻿using System;
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
    public class LanguagesController : Controller
    {
        private SazBakiEntities db = new SazBakiEntities();

        // GET: Admin/Languages
        public ActionResult Index()
        {
            return View(db.Languages.ToList());
        }

        // GET: Admin/Languages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Language language = db.Languages.Find(id);
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }

        // GET: Admin/Languages/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Admin/Languages/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,language1")] Language language)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Languages.Add(language);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(language);
        //}

        // GET: Admin/Languages/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Language language = db.Languages.Find(id);
        //    if (language == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(language);
        //}

        ////POST: Admin/Languages/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,language1")] Language language)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(language).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(language);
        //}

        //// GET: Admin/Languages/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Language language = db.Languages.Find(id);
        //    if (language == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(language);
        //}

        //// POST: Admin/Languages/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Language language = db.Languages.Find(id);
        //    db.Languages.Remove(language);
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
