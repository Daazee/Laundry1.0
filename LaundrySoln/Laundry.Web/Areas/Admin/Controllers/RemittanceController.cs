using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laundry.Model;
using Laundry.BLL;

namespace Laundry.Web.Areas.Admin.Controllers
{
    public class RemittanceController : Controller
    {
        private Remittance RemittanceObj = new Remittance();
        private RemittanceBs NewRemittanceBs = new RemittanceBs();
        private HashHelper NewHashHelper = new HashHelper();

        DateTime TestDate;
        double TestDouble;
        // GET: Admin/Remittance
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ConfirmRemittance()
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
        public ActionResult ConfirmRemittance(FormCollection collection)
        {
            @ViewBag.Message = "";
            DateTime RemitStartdate;
            DateTime RemitEnddate;
            //if (!DateTime.TryParse(NewHashHelper.ValidateDate(collection["txtStartDate"]), out RemitStartdate))
            //{
            //    ViewBag.Message = "Remit Start Date is not valid";
            //    return View();
            //}
            //if (!DateTime.TryParse(NewHashHelper.ValidateDate(collection["txtEndDate"]), out RemitStartdate))
            //{
            //    ViewBag.Message = "Remit End Date is not valid";
            //    return View();
            //}
            //RemitStartdate = Convert.ToDateTime(NewHashHelper.ValidateDate(collection["txtStartDate"]));
            //RemitEnddate = Convert.ToDateTime(NewHashHelper.ValidateDate(collection["txtEndDate"]));
            RemitStartdate = Convert.ToDateTime(collection["txtStartDate"]);
            RemitEnddate = Convert.ToDateTime(collection["txtEndDate"]);
            return View(NewRemittanceBs.GetRemittanceByDate(RemitStartdate, RemitEnddate));
        }

        public void CorfirmRemit(List<int> Ids, string Status)
        {
            //using (TransactionScope Trans = new TransactionScope())
            //{
            string result = "";
            try
            {
                if (Ids != null)
                {
                    foreach (var item in Ids)
                    {
                        //var myClothes = NewTransactionBs.GetById(item);
                        //myClothes.ClothStatus = Status;
                        //NewTransactionBs.Update(myClothes);
                        result = NewRemittanceBs.CorfirmRemit(item, Status);
                    }
                    Session["UpdateMessage"] = result;
                }
                else
                {
                    Session["UpdateMessage"] = "You selected nothing";

                }
                //  Trans.Complete();
            }
            catch (Exception ex)

            {
                throw new Exception(ex.Message);
            }
            // }
        }
    }
}