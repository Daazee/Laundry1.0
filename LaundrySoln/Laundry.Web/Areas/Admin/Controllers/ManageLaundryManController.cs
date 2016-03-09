using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laundry.Model;
using Laundry.BLL;

namespace Laundry.Web.Areas.Admin.Controllers
{
    public class ManageLaundryManController : Controller
    {
        LaundryManBs NewLaundryManBs = new LaundryManBs();
        // GET: Admin/ManageLaundryMan

        public ActionResult ListLaundryMan(string status)
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
            if (status == null)
            {
                status = "P";
            }
            ViewBag.status = status;
            if (status == "L")
            {
                return View(NewLaundryManBs.ListAll());
            }

            return View(NewLaundryManBs.ListAllByStatus(status));
        }

        public ActionResult UpdateStatus(string username, string status)
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
            var result = NewLaundryManBs.UpdateStatus(username, status);
            return RedirectToAction("ListLaundryMan");
        }
    }
}