using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laundry.BLL;
using Laundry.Model;

namespace Laundry.Web.Areas.Admin.Controllers
{
    public class CodeSettingsController : Controller
    {
        private CodesBs NewCodesBs = new CodesBs();
        Codes CodesObj = new Codes();
        // GET: Admin/CodeSettings
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
            return View();
        }

        public ActionResult CodesMenu()
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


        [HttpGet]
        public ActionResult CodesEntry(string code_type, string code_type_desc)
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
            ViewBag.code_type = code_type;
            ViewBag.code_type_desc = code_type_desc;
            return View(NewCodesBs.GetByCodeType(code_type));
        }

        [HttpPost]
        public ActionResult CodesEntry(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert logic here
                CodesObj.Codes_Type = collection["Codes_Type"];
                CodesObj.Codes_Desc = collection["Codes_Desc"];
                CodesObj.Codes_Val = collection["Codes_Val"];
                CodesObj.Codes_UserId = "admin";
                CodesObj.Codes_KeyDate = DateTime.Today;
                string flag = collection["Flag"];

                var ExistingCode = NewCodesBs.GetByCodeValue(CodesObj.Codes_Val);
                if (ExistingCode != null)
                {
                    ViewData["Message"] = "Code value already exist for descrption " + ExistingCode.Codes_Desc.ToString();
                    ViewBag.code_type = CodesObj.Codes_Type;
                    return View(NewCodesBs.GetByCodeType(CodesObj.Codes_Type));
                }

                var ExistingDesc = NewCodesBs.GetByCodeDesc(CodesObj.Codes_Desc);
                if (ExistingDesc != null)
                {
                    ViewData["Message"] = "Code description already exist for code value " + ExistingDesc.Codes_Val.ToString();
                    ViewBag.code_type = CodesObj.Codes_Type;
                    return View(NewCodesBs.GetByCodeType(CodesObj.Codes_Type));
                }

                if (flag == "false")
                {
                    CodesObj.Codes_Flag = "A";
                    NewCodesBs.Insert(CodesObj);
                    ViewData["Message"] = "Record save successfully";
                }
                else
                {
                    CodesObj.Codes_Flag = "C";
                    NewCodesBs.Update(CodesObj);
                    ViewData["Message"] = "Record updated successfully";
                }
            }
            ViewBag.code_type = CodesObj.Codes_Type;
            return View(NewCodesBs.GetByCodeType(CodesObj.Codes_Type));
        }



        [HttpGet]
        public ActionResult CodesUpdate(int id = 0)
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
            //ViewBag.code_type = code_type;
            //ViewBag.code_type_desc = code_type_desc;
            //return View(NewCodesBs.GetByCodeType(code_type));
            return View(NewCodesBs.GetById(id));
        }

        [HttpPost]
        public ActionResult CodesUpdate(int id, FormCollection collection)
        {
            string Codes_Type_Desc;
            try
            {

                             CodesObj.Codes_Type = collection["Codes_Type"];
                CodesObj.Codes_Desc = collection["Codes_Desc"];
                CodesObj.Codes_Val = collection["Codes_Val"];
                Codes_Type_Desc = collection["Codes_Type_Desc"];
                CodesObj.Codes_Id = id;
                CodesObj.Codes_UserId = "admin";
                CodesObj.Codes_Flag = "C";

                var ExistingCode = NewCodesBs.GetByCodeValue(CodesObj.Codes_Val);
                if (ExistingCode != null)
                {
                    ViewData["Message"] = "Code value already exist for descrption " + ExistingCode.Codes_Desc.ToString();
                    return RedirectToAction("CodesEntry", new { code_type = CodesObj.Codes_Type, code_type_desc = Codes_Type_Desc });
                }

                //var ExistingDesc = NewCodesBs.GetByCodeDesc(CodesObj.Codes_Desc);
                //if (ExistingDesc != null)
                //{
                //    ViewData["Message"] = "Code description already exist for code value " + ExistingDesc.Codes_Val.ToString();
                //    return RedirectToAction("CodesEntry", new { code_type = CodesObj.Codes_Type, code_type_desc = Codes_Type_Desc });
                //}

                NewCodesBs.Update(CodesObj);
                ViewData["Message"] = "Record updated successfully";

                return RedirectToAction("CodesEntry", new { code_type = CodesObj.Codes_Type, code_type_desc = Codes_Type_Desc });
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return View(NewCodesBs.GetByCodeType(CodesObj.Codes_Type));
            }
        }
    }
}