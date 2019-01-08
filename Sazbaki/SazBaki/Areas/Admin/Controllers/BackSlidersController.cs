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
    public class BackSlidersController : Controller
    {
        private SazBakiEntities db = new SazBakiEntities();

        // GET: Admin/BackSliders
        public ActionResult Index()
        {
            return View(db.BackSliders.ToList());
        }

        // GET: Admin/BackSliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BackSlider backSlider = db.BackSliders.Find(id);
            if (backSlider == null)
            {
                return HttpNotFound();
            }
            return View(backSlider);
        }

        // GET: Admin/BackSliders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BackSliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase imagefile)
        {
            Random rnd = new Random();
            var randonnumber = rnd.Next(10000, 99999);
            var InputFileName = Path.GetFileName(imagefile.FileName);
            var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + randonnumber + InputFileName);
            //Save file to server folder
            imagefile.SaveAs(ServerSavePath);

            BackSlider img = new BackSlider()
            {
                back_slider_img = randonnumber + InputFileName
            };
            db.BackSliders.Add(img);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/BackSliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BackSlider backSlider = db.BackSliders.Find(id);
            if (backSlider == null)
            {
                return HttpNotFound();
            }
            return View(backSlider);
        }

        // POST: Admin/BackSliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(int id, HttpPostedFileBase imagefile, string current_image_name)
        {
            var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + current_image_name);
            //Save file to server folder
            imagefile.SaveAs(ServerSavePath);
            
            return RedirectToAction("Index");
        }

        // GET: Admin/BackSliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BackSlider backSlider = db.BackSliders.Find(id);
            if (backSlider == null)
            {
                return HttpNotFound();
            }
            return View(backSlider);
        }

        // POST: Admin/BackSliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BackSlider backSlider = db.BackSliders.Find(id);
            db.BackSliders.Remove(backSlider);
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
