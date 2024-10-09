using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ArtSpot.Models;

namespace ArtSpot.Controllers
{
    public class ProductController : Controller
    {
        private artspotEntities db = new artspotEntities();

        // GET: Product
        public ActionResult Index()
        {
            var tbl_product = db.tbl_product.Include(t => t.tbl_category).Include(t => t.tbl_user);
            return View(tbl_product.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_product tbl_product = db.tbl_product.Find(id);
            if (tbl_product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.pro_fk_cat = new SelectList(db.tbl_category, "cat_id", "cat_name");
            ViewBag.pro_fk_user = new SelectList(db.tbl_user, "u_id", "u_name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pro_id,pro_name,pro_image,pro_des,pro_price,pro_contact,pro_fk_cat,pro_fk_user")] tbl_product ex)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["pro_image"];
                ex.pro_image = file.FileName;
                file.SaveAs(Server.MapPath("~/content/Product_Images/" + file.FileName));


                db.tbl_product.Add(ex);
                db.SaveChanges();
                return RedirectToAction("All_Art", "User");
            }

            ViewBag.pro_fk_cat = new SelectList(db.tbl_category, "cat_id", "cat_name", ex.pro_fk_cat);
            ViewBag.pro_fk_user = new SelectList(db.tbl_user, "u_id", "u_name", ex.pro_fk_user);
            return View(ex);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_product tbl_product = db.tbl_product.Find(id);
            if (tbl_product == null)
            {
                return HttpNotFound();
            }
            ViewBag.pro_fk_cat = new SelectList(db.tbl_category, "cat_id", "cat_name", tbl_product.pro_fk_cat);
            ViewBag.pro_fk_user = new SelectList(db.tbl_user, "u_id", "u_name", tbl_product.pro_fk_user);
            return View(tbl_product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pro_id,pro_name,pro_image,pro_des,pro_price,pro_contact,pro_fk_cat,pro_fk_user")] tbl_product tbl_product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pro_fk_cat = new SelectList(db.tbl_category, "cat_id", "cat_name", tbl_product.pro_fk_cat);
            ViewBag.pro_fk_user = new SelectList(db.tbl_user, "u_id", "u_name", tbl_product.pro_fk_user);
            return View(tbl_product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_product tbl_product = db.tbl_product.Find(id);
            if (tbl_product == null)
            {
                return HttpNotFound();
            }
            return View(tbl_product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_product tbl_product = db.tbl_product.Find(id);
            db.tbl_product.Remove(tbl_product);
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
