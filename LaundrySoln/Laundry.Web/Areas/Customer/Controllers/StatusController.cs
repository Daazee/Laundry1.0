using Laundry.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laundry.Web.Areas.Customer.Controllers
{
    public class StatusController : Controller
    {
        // GET: Customer/Status
        private TransactionBs NewTransactionBs = new TransactionBs();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CheckStatus(string MyTransNo)
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
            Session["MyTransNo"] = MyTransNo;
            return View(NewTransactionBs.GetByTransactionNo(MyTransNo));
        }
    }
}