using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laundry.BLL;
using Laundry.Model;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Data;
using Laundry.Web.ReportDataset;
//using System.Web.Http.Cors;

namespace Laundry.Web.Areas.User.Controllers
{
    public class TransactionController : Controller
    {
        private ClothingBs NewClothingBS = new ClothingBs();
        private TransactionBs NewTransactionBs = new TransactionBs();
        private PaymentDetailBs NewPaymentDetailBs = new PaymentDetailBs();
        CompanyDetailBs NewCompanyDetailBs = new CompanyDetailBs();
        private CodesBs NewCodesBs = new CodesBs();
        Transaction TransactionObj = new Transaction();
        PaymentDetail PaymentDetailObj = new PaymentDetail();
        ExpressCharge ExPressChargeObj = new ExpressCharge();
        ExpressChargeBs NewExPressChargeBs = new ExpressChargeBs();
        LaundryManBs NewLaundryManBs = new LaundryManBs();
        Receipt receipt = new Receipt();
        // GET: User/Transaction
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TransactionEntry()
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
            var Clothes = NewClothingBS.ListAll();
            ViewBag.Code_Desc = new SelectList(Clothes, "ClothId", "ClothDesc");
            ViewBag.Color = new SelectList(NewCodesBs.GetByCodeType("Cod001"), "Codes_Val", "Codes_Desc");
            return View();
        }

        [HttpPost]
        public ActionResult TransactionEntry(FormCollection collection)
        {
            var Clothes = NewClothingBS.ListAll();
            ViewBag.Code_Desc = new SelectList(Clothes, "ClothId", "ClothDesc");
            ViewBag.Color = new SelectList(NewCodesBs.GetByCodeType("Cod001"), "Codes_Val", "Codes_Desc");
            return View();
        }

        [HttpGet]
        public ActionResult TransEntry()
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
            var Clothes = NewClothingBS.ListAll();
            ViewBag.Code_Desc = new SelectList(Clothes, "ClothId", "ClothDesc");
            ViewBag.Color = new SelectList(NewCodesBs.GetByCodeType("Cod001"), "Codes_Val", "Codes_Desc");
            return View();
        }

        public ActionResult TransEntry(FormCollection collection)
        {
            var Clothes = NewClothingBS.ListAll();
            ViewBag.Code_Desc = new SelectList(Clothes, "ClothId", "ClothDesc");
            ViewBag.Color = new SelectList(NewCodesBs.GetByCodeType("Cod001"), "Codes_Val", "Codes_Desc");
            return View();
        }

        [HttpPost]
        public JsonResult Transact(Transaction TransObj)
        {
            try
            {
                ViewBag.UserId = Session["Username"].ToString();
            }
            catch
            {
                ViewBag.UserId = "";
            }

            if (TransObj.HeaderDetail == "H")
            {
                ExPressChargeObj.ExPressNo = PaymentDetailObj.PaymentNo = TransactionObj.TransactionNo = GenerateTransNo();
                PaymentDetailObj.CustomerName = TransactionObj.CustomerName = TransObj.CustomerName;
                PaymentDetailObj.AmountPaid = TransactionObj.AmountPaid = Convert.ToDouble(TransObj.AmountPaid);
                PaymentDetailObj.CustomerTag = TransactionObj.CustomerTag = TransObj.CustomerName + "/" + Convert.ToInt32(TransactionObj.TransactionNo.Split('/')[2]).ToString();
                ExPressChargeObj.ExpressFlag = PaymentDetailObj.Flag = TransactionObj.Flag = "A";
                ExPressChargeObj.ExpressKeyDate = PaymentDetailObj.KeyDate = TransactionObj.KeyDate = DateTime.Today;
                ExPressChargeObj.ExpressUserId = PaymentDetailObj.UserId = TransactionObj.UserId = ViewBag.UserId;
                NewPaymentDetailBs.Insert(PaymentDetailObj);

                if (TransObj.ExPressAmount.ToString() != "" && TransObj.ExPressAmount != 0)
                {
                    ExPressChargeObj.ExpressCharge_Id = Guid.NewGuid().ToString("N");
                    ExPressChargeObj.ExpressAmount = Convert.ToDouble(TransObj.ExPressAmount);
                    NewExPressChargeBs.Insert(ExPressChargeObj);
                }

            }
            else
            {
                TransactionObj.TransactionNo = Session["TransactionNo"].ToString();
            }

            if (TransObj.ExPressAmount.ToString() == "")
            {
                TransObj.ExPressAmount = 0;
            }


            TransactionObj.ClothCode = TransObj.ClothCode;
            TransactionObj.Amount = Convert.ToDouble(TransObj.Amount);
            TransactionObj.UnitPrice = Convert.ToDouble(TransObj.UnitPrice);
            TransactionObj.Colour = TransObj.Colour;
            TransactionObj.Address = TransObj.Address;
            TransactionObj.LaundryType = TransObj.LaundryType;
            TransactionObj.Quantity = TransObj.Quantity;
            TransactionObj.Balance = Convert.ToDouble(TransObj.Balance);
            TransactionObj.TotalCostAmount = Convert.ToDouble(TransObj.TotalCostAmount) + Convert.ToDouble(TransObj.ExPressAmount);
            TransactionObj.ExPressAmount = Convert.ToDouble(TransObj.ExPressAmount);
            TransactionObj.PaymentMode = TransObj.PaymentMode;
            //ViewBag.GenTransNo = TransactionObj.TransactionNo;
            string[] SplitResult = TransactionObj.TransactionNo.Split('/');
            TransactionObj.CustomerName = TransObj.CustomerName;
            TransactionObj.AmountPaid = Convert.ToDouble(TransObj.AmountPaid);
            TransactionObj.CustomerTag = TransObj.CustomerName + "/" + Convert.ToInt32(SplitResult[2]).ToString();
            TransactionObj.CustomerPhone = TransObj.CustomerPhone;
            TransactionObj.EmailAddress = TransObj.EmailAddress;
            TransactionObj.CollectionDate = TransObj.CollectionDate;
            TransactionObj.ClothStatus = "N";//New Cloth;
            TransactionObj.Flag = "A";
            TransactionObj.KeyDate = DateTime.Today;
            TransactionObj.UserId = ViewBag.UserId;
            string Tag = TransObj.CustomerName + "/" + Convert.ToInt32(SplitResult[2]).ToString();
            Session["TransactionNo"] = TransactionObj.TransactionNo;
            TransactionObj.HeaderDetail = TransObj.HeaderDetail;
            NewTransactionBs.Insert(TransactionObj);
            return Json(new { transNo = TransactionObj.TransactionNo, tag = Tag }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult _AddItem()
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
            var Clothes = NewClothingBS.ListAll();
            ViewBag.Color = new SelectList(NewCodesBs.GetByCodeType("Cod001"), "Codes_Val", "Codes_Desc");
            ViewBag.Code_Desc = new SelectList(Clothes, "ClothId", "ClothDesc");

            return PartialView();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult _AddItem(FormCollection collection)
        {
            var Clothes = NewClothingBS.ListAll();
            ViewBag.Code_Desc = new SelectList(Clothes, "ClothId", "ClothDesc");
            ViewBag.Color = new SelectList(NewCodesBs.GetByCodeType("Cod001"), "Codes_Val", "Codes_Desc");
            return PartialView();
        }


        public JsonResult GetUnitPrice(string id)
        {
            var result = NewClothingBS.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cost(double Price, int Quantity)
        {
            double TotalCost = Price * Quantity;
            return Json(TotalCost, JsonRequestBehavior.AllowGet);
        }

        public string GenerateTransNo()
        {
            string TransNo = "TN";
            string currentDay = DateTime.Today.Day.ToString();
            string currentMonth = DateTime.Today.Month.ToString();
            string currentYear = DateTime.Today.Year.ToString();

            if (currentDay.Length < 2)
                currentDay = "0" + currentDay;
            if (currentMonth.Length < 2)
                currentMonth = "0" + currentMonth;
            TransNo = TransNo + "/" + currentMonth + currentDay + currentYear;
            int LastTransNo = NewTransactionBs.GetLastTransNo(TransNo);
            int NewTransNo = LastTransNo + 1;
            TransNo = TransNo + "/" + NewTransNo.ToString();
            return TransNo;
        }

        [HttpGet]
        public ActionResult PrintReceipt()
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
            if (Session["TransactionNo"] != null)
            {
                TransactionObj.TransactionNo = Session["TransactionNo"].ToString();
            }
            // ViewBag.TransNo = Session["TransactionNo"].ToString();
            return View(TransactionObj);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult PrintReceipt(FormCollection frm)
        {
            // return View(NewTransactionBs.GetByTransactionNo(frm["TransNo"]));
            Session["FinalTransNo"] = frm["TransactionNo"];
         //   return RedirectToAction("PrintReceiptFinal"); 
                 return RedirectToAction("TransactionReceipt");
        }

        public FileResult ExportTo(string ReportType = "Pdf")
        {
            LocalReport localReport = new LocalReport();
            localReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
            localReport.ReportPath = Server.MapPath("~/Reports/TransactionReceipt.rdlc");
            //localReport.ReportPath = Server.MapPath("TransactionReceipt.rdlc");
            ReportDataSource reportDtSource = new ReportDataSource();
            reportDtSource.Name = "TransactionDataSet";
            reportDtSource.Value = NewTransactionBs.GetByTransactionNo(Session["FinalTransNo"].ToString()).ToList();

            localReport.DataSources.Add(reportDtSource);
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension = (ReportType == "Excel") ? "xlsx" : "pdf";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            renderedBytes = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension,
                                            out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment; filename=Receipt'" + DateTime.Now + "'." + fileNameExtension);
            return File(renderedBytes, fileNameExtension);
        }
        public FileResult ExportReceiptsTo(string ReportType = "Pdf")
        {
            LocalReport localReport = new LocalReport();
            localReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
            localReport.ReportPath = Server.MapPath("~/Reports/TransactionReceiptNew.rdlc");
            ReportDataSource reportDtSource = new ReportDataSource();
            DataRow dr;
            ReportDataSet ds = new ReportDataSet();
            LaundryMan LaundryManObj = new LaundryMan();
            string GetAttendantOnce = "Y";
            foreach (var receiptitem in NewTransactionBs.GetByTransactionNo(Session["FinalTransNo"].ToString()))
            {
                dr = ds.Tables["ReceiptDT"].Rows.Add();
                dr["TransactionNo"] = receiptitem.TransactionNo;
                dr["CustomerName"] = receiptitem.CustomerName;
                dr["ClothCode"] = receiptitem.ClothCode;
                dr["Amount"] = receiptitem.Amount;
                dr["LaundryType"] = receiptitem.LaundryType;
                dr["Quantity"] = receiptitem.Quantity;
                dr["CustomerTag"] = receiptitem.CustomerTag; ;

                dr["Colour"] = receiptitem.Colour;
                dr["Address"] = receiptitem.Address;
                dr["UnitPrice"] = receiptitem.UnitPrice;
                dr["AmountPaid"] = receiptitem.AmountPaid;
                dr["Balance"] = receiptitem.Balance;
                dr["ExPressAmount"] = receiptitem.ExPressAmount;
                dr["TotalCostAmount"] = receiptitem.TotalCostAmount;

                //Prevent visiting LaundryMan Dbset more than once
                if (GetAttendantOnce == "Y")
                {
                    LaundryManObj = NewLaundryManBs.GetByUsername(receiptitem.UserId);
                    GetAttendantOnce = "N";
                }
                dr["AttendantSurname"] = LaundryManObj.Surname;
                dr["AttendantOthername"] = LaundryManObj.Othername;
            }
            reportDtSource.Name = "ReportDataSet";
            reportDtSource.Value = ds.Tables["ReceiptDT"];
            localReport.DataSources.Add(reportDtSource);
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension = (ReportType == "Excel") ? "xlsx" : "pdf";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            renderedBytes = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension,
                                            out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment; filename=Receipt" + DateTime.Now + "." + fileNameExtension);
            return File(renderedBytes, fileNameExtension);
        }
        [HttpGet]
        public ActionResult PrintReceiptFinal(string TNo)
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
            if (Session["FinalTransNo"] != null)
            {
                return View(NewTransactionBs.GetByTransactionNo(Session["FinalTransNo"].ToString()));
            }
            return View();
        }

        [HttpGet]
        public ActionResult TransactionReceipt(string TNo)
        {
            string GetAttendantOnce = "Y";
            LaundryMan LaundryManReceipt = new LaundryMan();
           List<Transaction> TransactionReceipt = new List<Transaction>();
           List<CompanyDetail> CompanyDetailReceipt = new List<CompanyDetail>();
            List<PaymentDetail> PaymentDetailReceipt = new List<PaymentDetail>();
            try
            {
                ViewBag.UserId = Session["Username"].ToString();
            }
            catch
            {
                Session["ConfirmLogin"] = "You must login first";
                return RedirectToAction("Login", new { Area = "Security", Controller = "Access" });
            }
            if (Session["FinalTransNo"] != null)
            {
                TransactionReceipt = NewTransactionBs.GetByTransactionNo(Session["FinalTransNo"].ToString()).ToList();
                CompanyDetailReceipt = NewCompanyDetailBs.ListAll().ToList();
                PaymentDetailReceipt = NewPaymentDetailBs.GetByPaymentNo(Session["FinalTransNo"].ToString()).ToList();
                if (GetAttendantOnce == "Y")
                {
                    foreach(var item in TransactionReceipt)
                    LaundryManReceipt = NewLaundryManBs.GetByUsername(item.UserId);
                    GetAttendantOnce = "N"; 
                }
                //dr["AttendantSurname"] = LaundryManObj.Surname;
                //dr["AttendantOthername"] = LaundryManObj.Othername;
                receipt.TransactionReceipt = TransactionReceipt;
                receipt.CompanyDetailReceipt = CompanyDetailReceipt;
                receipt.LaundryManReceipt = LaundryManReceipt;
                receipt.PaymentDetailReceipt = PaymentDetailReceipt;
                return View(receipt);

            }
            return View();
        }

        [HttpGet]
        public ActionResult Search()
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

        // [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Search(string type, string value, string startdate, string enddate)
        {


            DateTime FinalStartDate = DateTime.Today, FinalEndDate = DateTime.Today;
            if (startdate != "" && DateTime.TryParse(startdate, out FinalStartDate))
            {
                //FinalStartDate = Convert.ToDateTime(ConvertToDateFormat(startdate));
                FinalStartDate = Convert.ToDateTime(startdate);
            }

            if (enddate != "" && DateTime.TryParse(enddate, out FinalEndDate))
            {
                FinalEndDate = Convert.ToDateTime(enddate);
            }

            return View(NewTransactionBs.Search(type, value, FinalStartDate, FinalEndDate));
        }

        private string ConvertToDateFormat(string dateValue)
        {
            string newDate = string.Empty;
            if (dateValue != string.Empty)
            {
                string[] dDate = dateValue.Split('/');
                newDate = dDate[2] + "-" + dDate[1] + "-" + dDate[0];
            }
            return newDate;
        }


        [HttpGet]
        public ActionResult TransUpdate(string transNo = "")
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
            ViewBag.MyTransNo = transNo;
          //  return View(NewTransactionBs.GetSingleTransaction(transNo));
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult TransUpdate(FormCollection collection)
        {
            try
            {
                ViewBag.UserId = Session["Username"].ToString();
            }
            catch
            {
                ViewBag.UserId = "";
            }
            PaymentDetailObj.AmountPaid = TransactionObj.Balance = Convert.ToDouble(collection["Balance"]);
            PaymentDetailObj.PaymentNo = TransactionObj.TransactionNo = collection["TransactionNo"];
            PaymentDetailObj.CustomerName = TransactionObj.CustomerName = collection["CustomerName"] = collection["CustName"];
            PaymentDetailObj.CustomerTag = TransactionObj.CustomerTag = collection["CustomerTag"] = collection["CustTag"];
            PaymentDetailObj.Flag = "A";
            PaymentDetailObj.KeyDate = DateTime.Today;
            PaymentDetailObj.UserId = ViewBag.UserId;
            NewTransactionBs.TransactionUpdate(TransactionObj);
            NewPaymentDetailBs.Insert(PaymentDetailObj);
            ViewData["Message"] = "Record updated successfully";
            return View();
        }

        [HttpGet]
        public ActionResult Collection()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Collection(FormCollection collection)
        {
            // tr
            return View();
        }

        public JsonResult GetTransactionSummary(string TransNo)
        {
            return Json(NewTransactionBs.GetByTransactionNo(TransNo), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UpdateClothStatus(string MyTransNo)
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
            //return View();
        }

        //Get can actually post to itself, Below acion anem is not needed
        // [HttpGet]
        //// [ActionName("UpdateClothStatus")]
        // //[]
        // //Note Action Name is different by an underscore(_)
        // public ViewResult UpdateCloth_Status(string MyTransNo)
        // {
        //      Session["MyTransNo"] = MyTransNo;
        //     return View(NewTransactionBs.GetByTransactionNo(MyTransNo));
        // }

        public ActionResult UpdateStatusReady(int Transid, string status)
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
            var result = NewTransactionBs.UpdateClothStatus(Transid, status);
            Session["UpdateMessage"] = result;
            return RedirectToAction("UpdateClothStatus", new { MyTransNo = Session["MyTransNo"] });
        }

       // [AllowCrossSiteJson]
        public void ReadyOrCollection(List<int> Ids, string Status)
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
                        result = NewTransactionBs.UpdateClothStatus(item, Status);
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

        public ActionResult t()
        {
            return View();
        }

        void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            LocalReport localReport = new LocalReport();
            //e.ReportPath = Server.MapPath("~/Reports/TransactionReceipt.rdlc");
            //ReportDataSource reportDtSource = new ReportDataSource();
            //reportDtSource.Name = "TransactionDataSet";
            //reportDtSource.Value = NewTransactionBs.GetByTransactionNo(Session["FinalTransNo"].ToString()).ToList();

            //localReport.DataSources.Add(reportDtSource);
            // int iEmpID = Convert.ToInt32(e.Parameters[0].Values[0]);

            DataRow dr;
            ReportDataSet ds = new ReportDataSet();
            //IEnumerable<CompanyDetail> res;
            // res = IEnumerable < CompanyDetail > NewCompanyDetailBs.GetById(1);

            /*var result = NewCompanyDetailBs.GetById(1);

            dr = ds.Tables["CompanyDT"].Rows.Add();
            dr["CompanyName"] = result.Company_Name;
            dr["CompanyAddress"] = result.Company_Address;*/

            e.DataSources.Add(new ReportDataSource()
            {
                Name = "CompDetDataSet",
                Value = NewCompanyDetailBs.GetByIdList(1)
            });

            //e.DataSources.Add(new ReportDataSource()
            //{
            //    Name = "CompanyDT",
            //    Value =ds.Tables["CompanyDT"]
            //});


        }

        //Get Total Income made by date
        public ActionResult GetByPaymentDate_UserId(string date, string userId)
        {
            ViewBag.ErrorMsg = "";
            if (date == "")
            {
                ViewBag.ErrorMsg = "Date must  not be empty";
                return View();
            }
            var result = NewPaymentDetailBs.GetByPaymentDate_UserId(date, userId);
            double totalsales = 0.0;
            if (result != null)
            {
                foreach (var amount in result)
                {
                    totalsales += amount.AmountPaid;
                }
            }

            return Json(totalsales, JsonRequestBehavior.AllowGet);
        }
    }
}