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
    public class GalleriesController : Controller
    {
        private SazBakiEntities db = new SazBakiEntities();

        // GET: Admin/Galleries
        public ActionResult Index()
        {
            return View(db.Galleries.ToList());
        }

        // GET: Admin/Galleries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // GET: Admin/Galleries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Galleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,gallery_img_name")] Gallery gallery, HttpPostedFileBase imagefile)
        {

            Random rnd = new Random();
            var randonnumber = rnd.Next(10000, 99999);
            var InputFileName = Path.GetFileName(imagefile.FileName);
            var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + randonnumber + InputFileName);
            //Save file to server folder
            imagefile.SaveAs(ServerSavePath);

            if (ModelState.IsValid)
            {
                gallery.gallery_img = randonnumber + InputFileName;
                db.Galleries.Add(gallery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gallery);
        }

        // GET: Admin/Galleries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Admin/Galleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string gallery_img_name, HttpPostedFileBase imagefile, string current_image_name)
        {            
            if (ModelState.IsValid)
            {
                if (imagefile!=null)
                {
                    var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + current_image_name);
                    //Save file to server folder
                    imagefile.SaveAs(ServerSavePath);

                    var img = db.Galleries.Find(id);
                    img.gallery_img_name = gallery_img_name;
                    db.SaveChanges();
                }
                else
                {
                    var img = db.Galleries.Find(id);
                    img.gallery_img_name = gallery_img_name;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Admin/Galleries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: Admin/Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gallery gallery = db.Galleries.Find(id);
            db.Galleries.Remove(gallery);
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
