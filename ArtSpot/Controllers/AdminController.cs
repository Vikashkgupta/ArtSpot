using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ArtSpot.Models;

namespace ArtSpot.Controllers
{
    public class AdminController : Controller
    {

        artspotEntities db = new artspotEntities();

        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tbl_admin lg)
        {
            try
            {
                tbl_admin t1 = db.tbl_admin.Where(x => x.ad_username == lg.ad_username && x.ad_password == lg.ad_password).SingleOrDefault();
                if (t1 != null)
                {
                    Session["aid"] = lg.ad_username;                //  set of session  

                    Response.Write("<script>alert(' Welcome to Admin Zone  '); window.location.href='/admin/admin_dashboard' </script>");
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

        public ActionResult Admin_Dashboard()
        {
            return View();
        }
    }
}