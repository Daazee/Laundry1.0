using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laundry.Model;
using Laundry.BLL;


namespace Laundry.Web.Areas.User.Controllers
{
    public class RemittanceController : Controller
    {
        private Remittance RemittanceObj = new Remittance();
        private RemittanceBs NewRemittanceBs = new RemittanceBs();
        private HashHelper NewHashHelper = new HashHelper();

        DateTime TestDate;
        double TestDouble;
        // GET: User/Remittance
        public ActionResult RemittanceMenu()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Remit()
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remit(FormCollection collection)
        {

            //try
            //{
            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.UserId = Session["Username"].ToString();
                }
                catch
                {
                    ViewBag.UserId = "";
                }
                //if (!DateTime.TryParse(NewHashHelper.ValidateDate(collection["Remittance_Date"]), out TestDate))
                //{
                //    ViewBag.ErrorMsg = "Remit Date is not valid";

                //}
                if (!DateTime.TryParse(collection["Remittance_Date"], out TestDate))
                {
                    ViewBag.ErrorMsg = "Remit Date is not valid";

                }
                else if (!double.TryParse(collection["Remittance_Amount"], out TestDouble))
                {
                    ViewBag.ErrorMsg = "Remit Amount is not valid";
                }
                else
                {
                    //RemittanceObj.Remittance_Date = Convert.ToDateTime(NewHashHelper.ValidateDate(collection["Remittance_Date"]));
                    //RemittanceObj.Remittance_Amount = Convert.ToDouble(collection["Remittance_Amount"]);
                    RemittanceObj.Remittance_Date = Convert.ToDateTime(collection["Remittance_Date"]);
                    RemittanceObj.Remittance_Amount = Convert.ToDouble(collection["Remittance_Amount"]);
                    RemittanceObj.Special_Note = collection["Special_Note"];
                    RemittanceObj.Remittance_Status = "Pending";
                    RemittanceObj.Remittance_UserId = ViewBag.UserId;
                    RemittanceObj.Remittance_KeyDate = DateTime.Today;
                    string flag = collection["Flag"];
                    if (flag == "false")
                    {
                        RemittanceObj.Remittance_Flag = "A";
                        NewRemittanceBs.Insert(RemittanceObj);
                        ViewData["Message"] = "Record save successfully";
                    }
                    else
                    {
                        RemittanceObj.Remittance_Flag = "C";
                        NewRemittanceBs.Update(RemittanceObj);
                        ViewData["Message"] = "Record updated successfully";
                    }
                }
            }
            return View();
        }

        public ActionResult GetByRemitDate_UserId(string date, string userId)
        {
            ViewBag.ErrorMsg = "";
            if (date == "")
            {
                ViewBag.ErrorMsg = "Date must  not be empty";
                return View();
            }
            var result = NewRemittanceBs.GetByRemitDate_UserId(date, userId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult MyRemittance()
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
            return View();
        }

        [HttpPost]
        public ActionResult MyRemittance(FormCollection collection)
        {
            @ViewBag.Message = "";
            DateTime RemitStartdate;
            DateTime RemitEnddate;
            /* if (!DateTime.TryParse(NewHashHelper.ValidateDate(collection["txtStartDate"]), out RemitStartdate))
             {
                 ViewBag.Message = "Remit Start Date is not valid";
                 return View();
             }
             if (!DateTime.TryParse(NewHashHelper.ValidateDate(collection["txtEndDate"]), out RemitStartdate))
             {
                 ViewBag.Message = "Remit End Date is not valid";
                 return View();
             }*/
            try
            {
                ViewBag.UserId = Session["Username"].ToString();
            }
            catch
            {
                ViewBag.UserId = "";
            }
            //RemitStartdate = Convert.ToDateTime(NewHashHelper.ValidateDate(collection["txtStartDate"]));
            //RemitEnddate = Convert.ToDateTime(NewHashHelper.ValidateDate(collection["txtEndDate"]));
            RemitStartdate = Convert.ToDateTime(collection["txtStartDate"]);
            RemitEnddate = Convert.ToDateTime(collection["txtEndDate"]);
            return View(NewRemittanceBs.GetMyRemittance(RemitStartdate, RemitEnddate, ViewBag.UserId));
        }

    }

}