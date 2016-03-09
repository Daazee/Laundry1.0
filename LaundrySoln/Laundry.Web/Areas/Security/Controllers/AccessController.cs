using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laundry.Model;
using Laundry.BLL;


namespace Laundry.Web.Areas.Security.Controllers
{
    public class AccessController : Controller
    {
        LaundryManBs NewLaundryManBs = new LaundryManBs();
        LaundryMan LaundryManObj = new LaundryMan();
        CompanyDetailBs NewCompanyDetailBs = new CompanyDetailBs();

        // GET: Security/Access
        public ActionResult Index()
        {
            return View();
        }

        // GET: Security/Access/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Security/Access/Create
        public ActionResult Login()
        {
            var CompanyShortName = NewCompanyDetailBs.DisplayCompanyShortName();
            ViewBag.CompanyShortName = CompanyShortName;
            return View();
        }

        // POST: Security/Access/Create
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                Session["ConfirmLogin"] = "";
                LaundryManObj.Username = collection["Username"];
                LaundryManObj.Password = collection["Password"];
                var result = NewLaundryManBs.Login(LaundryManObj.Username, LaundryManObj.Password);
                if (result == "Success")
                {
                    Session["Username"] = collection["Username"];
                    Session["ConfirmLogin"] = "";
                    var verify_admin_laundryman = NewLaundryManBs.GetByUsername(LaundryManObj.Username);
                    //AD denotes admin status by manully entering in db
                    if(verify_admin_laundryman.Reg_Status=="AD")
                        return RedirectToAction("Index", new { Controller = "Menu", Area = "Admin" });
                    else
                    return RedirectToAction("Index", new { Controller="Menu", Area="User"});
                }
                else
                {
                    ViewData["Message"] = result;
                    return View(LaundryManObj);
                }
            }
            else
            {
                return View(LaundryManObj);
            }
           // return View();
        }

        public ActionResult Logout()
        {
            if (Session["Username"] != null)

            {
                Session.Remove(Session["Username"].ToString());
                Session.Remove(Session["ConfirmLogin"].ToString());
                Session.Abandon();
            }
                return RedirectToAction("Login", new { Area = "Security", Controller = "Access" });
          
        }
        // GET: Security/Access/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Security/Access/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Security/Access/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Security/Access/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
