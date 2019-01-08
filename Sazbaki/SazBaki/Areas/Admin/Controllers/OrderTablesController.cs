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
    public class OrderTablesController : Controller
    {
        private SazBakiEntities db = new SazBakiEntities();

        // GET: Admin/OrderTables
        public ActionResult Index()
        {
            var orderTables = db.OrderTables.Include(o => o.Language);
            return View(orderTables.ToList());
        }

        // GET: Admin/OrderTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTable orderTable = db.OrderTables.Find(id);
            if (orderTable == null)
            {
                return HttpNotFound();
            }
            return View(orderTable);
        }

        //// GET: Admin/OrderTables/Create
        //public ActionResult Create()
        //{
        //    ViewBag.order_lang_id = new SelectList(db.Languages, "Id", "language1");
        //    return View();
        //}

        //// POST: Admin/OrderTables/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,order_text,order_lang_id")] OrderTable orderTable, HttpPostedFileBase imagefile)
        //{
        //    Random rnd = new Random();
        //    var randonnumber = rnd.Next(10000, 99999);
        //    var InputFileName = Path.GetFileName(imagefile.FileName);
        //    var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + randonnumber + InputFileName);
        //    //Save file to server folder
        //    imagefile.SaveAs(ServerSavePath);


        //    if (ModelState.IsValid)
        //    {
        //        orderTable.order_img = randonnumber + InputFileName;
        //        db.OrderTables.Add(orderTable);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.order_lang_id = new SelectList(db.Languages, "Id", "language1", orderTable.order_lang_id);
        //    return View(orderTable);
        //}

        // GET: Admin/OrderTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTable orderTable = db.OrderTables.Find(id);
            if (orderTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.order_lang_id = new SelectList(db.Languages, "Id", "language1", orderTable.order_lang_id);
            return View(orderTable);
        }

        // POST: Admin/OrderTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string order_text, int order_lang_id, HttpPostedFileBase imagefile, string current_image_name)
        {
            if (ModelState.IsValid)
            {
                if (imagefile!=null)
                {
                    var ServerSavePath = Path.Combine(Server.MapPath("~/Uploads/") + current_image_name);
                    //Save file to server folder
                    imagefile.SaveAs(ServerSavePath);

                    var order = db.OrderTables.Find(id);
                    order.order_text = order_text;
                    order.order_lang_id = order_lang_id;
                    db.SaveChanges();
                    
                }
                else
                {
                    var order = db.OrderTables.Find(id);
                    order.order_text = order_text;
                    order.order_lang_id = order_lang_id;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.order_lang_id = new SelectList(db.Languages, "Id", "language1", order_lang_id);
            return View();
        }

        //// GET: Admin/OrderTables/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    OrderTable orderTable = db.OrderTables.Find(id);
        //    if (orderTable == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(orderTable);
        //}

        //// POST: Admin/OrderTables/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    OrderTable orderTable = db.OrderTables.Find(id);
        //    db.OrderTables.Remove(orderTable);
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
