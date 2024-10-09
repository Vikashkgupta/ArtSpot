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
    public class CategoryController : Controller
    {
        private artspotEntities db = new artspotEntities();

        // GET: Category
        public ActionResult Index()
        {
            var tbl_category = db.tbl_category.Include(t => t.tbl_admin);
            return View(tbl_category.ToList());
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_category tbl_category = db.tbl_category.Find(id);
            if (tbl_category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            ViewBag.cat_fk_ad = new SelectList(db.tbl_admin, "ad_id", "ad_username");
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cat_id,cat_name,cat_image,cat_fk_ad")] tbl_category ex)
        {
            try
            {
                if (ModelState.IsValid)
            {
                    HttpPostedFileBase file = Request.Files["cat_image"];
                    ex.cat_image = file.FileName;
                    file.SaveAs(Server.MapPath("~/content/Cat_Images/" + file.FileName));


                    db.tbl_category.Add(ex);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cat_fk_ad = new SelectList(db.tbl_admin, "ad_id", "ad_username", ex.cat_fk_ad);
            }
            catch (Exception exe)
            {
                Response.Write("<script>alert('Something went wrong !'); window.location.href='/Admin/MaterialMGMT' </script>");
            }
            return View(ex);

        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_category tbl_category = db.tbl_category.Find(id);
            if (tbl_category == null)
            {
                return HttpNotFound();
            }
            ViewBag.cat_fk_ad = new SelectList(db.tbl_admin, "ad_id", "ad_username", tbl_category.cat_fk_ad);
            return View(tbl_category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cat_id,cat_name,cat_image,cat_fk_ad")] tbl_category tbl_category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cat_fk_ad = new SelectList(db.tbl_admin, "ad_id", "ad_username", tbl_category.cat_fk_ad);
            return View(tbl_category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_category tbl_category = db.tbl_category.Find(id);
            if (tbl_category == null)
            {
                return HttpNotFound();
            }
            return View(tbl_category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_category tbl_category = db.tbl_category.Find(id);
            db.tbl_category.Remove(tbl_category);
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
