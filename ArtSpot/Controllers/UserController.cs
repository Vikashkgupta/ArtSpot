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
    public class UserController : Controller
    {
        private artspotEntities db = new artspotEntities();

        // GET: User
        public ActionResult Index()
        {
            return View(db.tbl_user.ToList());
        }
        public ActionResult User_Dashboard()
        {
            return View();
        }
        // GET: User/Browse/5
        //public ActionResult All_Art()
        //{
        //    var tbl_product = db.tbl_product.Include(t => t.tbl_category).Include(t => t.tbl_user);
        //    return View(tbl_product.ToList());


        //}
        public ActionResult All_Art(string searchBy, string search)
        {
            IQueryable<tbl_product> products = db.tbl_product.Include(t => t.tbl_category).Include(t => t.tbl_user);

            if (!string.IsNullOrEmpty(search))
            {
                if (searchBy == "name")
                {
                    products = products.Where(p => p.pro_name.StartsWith(search));
                }
                else if (searchBy == "category")
                {
                    products = products.Where(p => p.tbl_category.cat_name.StartsWith(search));
                }
            }

            return View(products.ToList());
        }
        public ActionResult Pro_Details(int? id)
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
        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_user tbl_user = db.tbl_user.Find(id);
            if (tbl_user == null)
            {
                return HttpNotFound();
            }
            return View(tbl_user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "u_id,u_name,u_email,u_password,u_image,u_contact")] tbl_user ex)
        {
            try
            {
                if (ModelState.IsValid)
            {

                    HttpPostedFileBase file = Request.Files["u_image"];
                    ex.u_image = file.FileName;
                    file.SaveAs(Server.MapPath("~/content/User_Images/" + file.FileName));


                    db.tbl_user.Add(ex);
                db.SaveChanges();
                return RedirectToAction("User_Login", "Home");
            }
            }
            catch (Exception exe)
            {
                Response.Write("<script>alert('Something went wrong !'); window.location.href='/user/create' </script>");
            }
            return View(ex);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_user tbl_user = db.tbl_user.Find(id);
            if (tbl_user == null)
            {
                return HttpNotFound();
            }
            return View(tbl_user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "u_id,u_name,u_email,u_password,u_image,u_contact")] tbl_user tbl_user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_user tbl_user = db.tbl_user.Find(id);
            if (tbl_user == null)
            {
                return HttpNotFound();
            }
            return View(tbl_user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_user tbl_user = db.tbl_user.Find(id);
            db.tbl_user.Remove(tbl_user);
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
