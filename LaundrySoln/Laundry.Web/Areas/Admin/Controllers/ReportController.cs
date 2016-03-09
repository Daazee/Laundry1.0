using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laundry.BLL;
using Microsoft.Reporting.WebForms;
using System.Data;
using Laundry.Web.ReportDataset;

namespace Laundry.Web.Areas.Admin.Controllers
{
    public class ReportController : Controller
    {
        ReportBs NewReportBs = new ReportBs();
        HashHelper helper = new HashHelper();
        CompanyDetailBs NewCompanyDetailBs = new CompanyDetailBs();
        // GET: Admin/Report
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //public ActionResult SalesReport()
        //{
        //    try
        //    {
        //        ViewBag.UserId = Session["Username"].ToString();
        //    }
        //    catch
        //    {
        //        Session["ConfirmLogin"] = "You must login first";
        //        return RedirectToAction("Login", new { Area = "Security", Controller = "Access" });
        //    }
        //    return View();
        //}
        public ActionResult SalesReport(string txtStartDate = "01/01/1900", string txtEndDate = "01/01/1900", string PageNo = null)
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
            DateTime StartDate = DateTime.Today;
            DateTime EndDate = DateTime.Today;
            int pageNo = int.Parse(PageNo == null ? "1" : PageNo);
            ViewBag.CurrentPage = pageNo;
            ViewBag.StartDate = txtStartDate;
            ViewBag.EndDate = txtEndDate;
            Session["StartDate"] = StartDate = Convert.ToDateTime(txtStartDate);
            Session["EndDate"] = EndDate = Convert.ToDateTime(txtEndDate);
            ViewBag.TotalPages = NewReportBs.SalesReportPages(StartDate, EndDate);
            return View(NewReportBs.SalesReport(StartDate, EndDate).Skip((pageNo - 1) * 10).Take(10));
        }
        //[HttpPost]
        //public ActionResult SalesReport(FormCollection frm)
        //{
        //    DateTime StartDate = DateTime.Today;
        //    DateTime EndDate = DateTime.Today;
        //    //Session["StartDate"] = StartDate = Convert.ToDateTime(frm["txtStartDate"]);
        //    // Session["EndDate"] =EndDate = Convert.ToDateTime(frm["txtEndDate"]);

        //    //Session["StartDate"] = StartDate = Convert.ToDateTime(helper.ValidateDate(frm["txtStartDate"]));
        //    //Session["EndDate"] = EndDate = Convert.ToDateTime(helper.ValidateDate(frm["txtEndDate"]));

        //    Session["StartDate"] = StartDate = Convert.ToDateTime(frm["txtStartDate"]);
        //    Session["EndDate"] = EndDate = Convert.ToDateTime(frm["txtEndDate"]);
        //    return View(NewReportBs.SalesReport(StartDate, EndDate));
        //}

       
        public FileResult ExportSalesReport(string ReportType = "Pdf")
        {
            LocalReport localReport = new LocalReport();
            localReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
            localReport.ReportPath = Server.MapPath("~/Reports/SalesReportNew.rdlc");
            ReportDataSource reportDtSource = new ReportDataSource();

            DataRow dr;
            ReportDataSet ds = new ReportDataSet();

            foreach (var sale in NewReportBs.SalesReport(Convert.ToDateTime(Session["StartDate"]), Convert.ToDateTime(Session["EndDate"])))
            {
                dr = ds.Tables["SalesReportDT"].Rows.Add();
                dr["AmountPaid"] = sale.AmountPaid;
                dr["CustomerName"] = sale.CustomerName;
                dr["CustomerTag"] = sale.CustomerTag;
                dr["KeyDate"] = sale.KeyDate;
                dr["PaymentNo"] = sale.PaymentNo;
                dr["StartDate"] = Session["StartDate"];
                dr["EndDate"] = Session["EndDate"];
            }

            //reportDtSource.Name = "SalesReportDataSet";
            //reportDtSource.Value = NewReportBs.SalesReport(Convert.ToDateTime(Session["StartDate"]), Convert.ToDateTime(Session["EndDate"]));
            reportDtSource.Name = "ReportDataSet";
            reportDtSource.Value= ds.Tables["SalesReportDT"];

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
            Response.AddHeader("content-disposition", "attachment; filename=SalesReport " + DateTime.Now + "." + fileNameExtension);
            return File(renderedBytes, fileNameExtension);
        }

        void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            LocalReport localReport = new LocalReport();
            e.DataSources.Add(new ReportDataSource()
            {
                Name = "CompDetDataSet",
                Value = NewCompanyDetailBs.GetByIdList(1)
            });
        }
    }
}