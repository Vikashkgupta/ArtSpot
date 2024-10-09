using ArtSpot.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace ArtSpot.Controllers
{
    public class HomeController : Controller
    {
        artspotEntities db = new artspotEntities();

        //public ActionResult Index()
        //{
        //    var tbl_product = db.tbl_product.Include(t => t.tbl_category).Include(t => t.tbl_user);
        //    return View(tbl_product.ToList());
        //}
        public ActionResult Index(string searchBy, string search)
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


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult User_Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult User_Login(tbl_user lg)
        {
            try
            {
                tbl_user t1 = db.tbl_user.Where(x => x.u_email == lg.u_email && x.u_password == lg.u_password).SingleOrDefault();
                if (t1 != null)
                {
                    Session["aid"] = lg.u_email;                //  set of session  

                    Response.Write("<script>alert(' Welcome to your dashbord  '); window.location.href='/user/All_Art' </script>");
                }
                else
                {
                    Response.Write("<script>alert('Invalid Username or Password ! ')</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('There are Error in code !  ! ')</script>");
            }

            return View();
        }

    }
}