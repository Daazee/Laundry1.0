using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laundry.BLL;
namespace Laundry.Web.Areas.Common.Controllers
{
    public class HomeController : Controller
    {
        CompanyDetailBs NewCompanyDetailBs = new CompanyDetailBs();
        // GET: Common/Home
        public ActionResult Index()
        {
            var CompanyName = NewCompanyDetailBs.DisplayCompanyName();
            ViewBag.CompanyName = CompanyName;
            return View();
        }
    }
}