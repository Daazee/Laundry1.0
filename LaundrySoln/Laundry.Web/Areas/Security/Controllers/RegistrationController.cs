using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laundry.Model;
using Laundry.BLL;

namespace Laundry.Web.Areas.Security.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Security/Registration
        LaundryManBs NewLaundryManBs = new LaundryManBs();
        LaundryMan LaundryManObj = new LaundryMan();

        CompanyDetailBs NewCompanyDetailBs = new CompanyDetailBs();
        CompanyDetail CompanyDetailObj = new CompanyDetail();
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

        // GET: Security/Registration/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Security/Registration/Create
        public ActionResult LaundryManRegistration(string ValidateReg="")
        {
            if(ValidateReg!="Y")
            {
                return RedirectToAction("Login", new { Area = "Security", Controller = "Access" });
            }
            return View();
        }

        // POST: Security/Registration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LaundryManRegistration(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                LaundryManObj.Surname = collection["Surname"];
                LaundryManObj.Othername = collection["Othername"];
                LaundryManObj.Sex = collection["Sex"];
                LaundryManObj.PhoneNumber = collection["PhoneNumber"];
                LaundryManObj.Address = collection["Address"];
                LaundryManObj.Username = collection["Username"];
                LaundryManObj.Password = collection["Password"];


                if (collection["Password"] == collection["ConfirmPassword"])
                {
                    var result = NewLaundryManBs.GetByUsername(LaundryManObj.Username);

                    if (result != null)
                    {
                        ViewData["Message"] = "Username already exist";
                        return View(LaundryManObj);
                    }
                    LaundryManObj.Reg_Status = "P";
                    //LaundryManObj.UserId = "admin";
                    LaundryManObj.Keydate = DateTime.Now;
                    LaundryManObj.Flag = "A";
                    NewLaundryManBs.Insert(LaundryManObj);
                    ViewData["Message"] = "Record save successfully";
                    return View();
                }
                else
                {
                    ViewData["Message"] = "Password does not matches";
                    //return RedirectToAction("LaundryManRegistration");
                    return View(LaundryManObj);
                }
            }
            else
            {
                return View();
            }
            //return View();
        }

        public ActionResult CompanyRegistration()
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
            return View(NewCompanyDetailBs.ListAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyRegistration(FormCollection collection)
        {
            if (ModelState.IsValid)
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

                if (collection["Company_Password"] == collection["ConfirmPassword"])
                {

                    CompanyDetailObj.Company_Name = collection["Company_Name"];
                    CompanyDetailObj.Company_ShortName = collection["Company_ShortName"];
                    CompanyDetailObj.Company_Address = collection["Company_Address"];
                    CompanyDetailObj.Company_Phone1 = collection["Company_Phone1"];
                    CompanyDetailObj.Company_Phone2 = collection["Company_Phone2"];
                    CompanyDetailObj.Company_Email = collection["Company_Email"];
                    CompanyDetailObj.Company_Username = collection["Company_Username"];
                    CompanyDetailObj.Company_Password = collection["Company_Password"];
                    CompanyDetailObj.Company_UserId = "admin";
                    CompanyDetailObj.Company_KeyDate = DateTime.Now;
                    CompanyDetailObj.Company_Flag = "A";
                    NewCompanyDetailBs.Insert(CompanyDetailObj);
                    ViewData["Message"] = "Record save successfully";
                    return View(NewCompanyDetailBs.ListAll());
                }
                else
                {
                    ViewData["Message"] = "Password does not matches";
                    //return RedirectToAction("LaundryManRegistration");
                    return View(CompanyDetailObj);
                }
            }
            else
            {
                return View();
            }
            //return View();
        }

        [HttpGet]
        public ActionResult CompanyRegUpdate(int id = 0)
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
            return View(NewCompanyDetailBs.GetById(id));
        }

        [HttpPost]
        public ActionResult CompanyRegUpdate(int id, FormCollection collection)
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

            CompanyDetailObj.Company_Id = Convert.ToInt32(collection["Company_Id"]);
            CompanyDetailObj.Company_Name = collection["Company_Name"];
            CompanyDetailObj.Company_ShortName = collection["Company_ShortName"];
            CompanyDetailObj.Company_Address = collection["Company_Address"];
            CompanyDetailObj.Company_Phone1 = collection["Company_Phone1"];
            CompanyDetailObj.Company_Phone2 = collection["Company_Phone2"];
            CompanyDetailObj.Company_Email = collection["Company_Email"];
            //CompanyDetailObj.Company_Username = collection["Company_Username"];
            //CompanyDetailObj.Company_Password = collection["Company_Password"];
            CompanyDetailObj.Company_UserId = "admin";
            CompanyDetailObj.Company_Flag = "C";
            NewCompanyDetailBs.Update(CompanyDetailObj);
            ViewData["Message"] = "Record save successfully";
            return RedirectToAction("CompanyRegistration");
        }

        public JsonResult VerifyUsername(string id)
        {
            var result = NewLaundryManBs.VerifyUsername(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerifyCompanyEmail(string email)
        {
            var result = NewCompanyDetailBs.VerifyCompanyEmail(email);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Security/Registration/Edit/5
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

        // GET: Security/Registration/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Security/Registration/Delete/5
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
        [HttpGet]
        public ActionResult AdminReg()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminReg(FormCollection collection)
        {
            return View();
        }
    }
}
