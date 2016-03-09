using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laundry.Web.Areas.User.Controllers
{
    public class MenuController : Controller
    {
        // GET: User/Menu
        public ActionResult Index()
        {
            try
            {
                ViewBag.UserId = Session["Username"].ToString();
            }
            catch
            {
                Session["ConfirmLogin"] = "You must login first";
                return RedirectToAction("Login", new { Area = "Security", Controller = "Access" });
            }
            ViewBag.Title = "User Menu";
            return View();   
        }
    }
}