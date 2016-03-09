using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laundry.Model;
using Laundry.BLL;

namespace Laundry.Web.Areas.Admin.Controllers
{
    public class ClothingUpdateController : Controller
    {
        private Clothing ClothingObj = new Clothing();
        private ClothingBs NewClothingBs = new ClothingBs();       // GET: Admin/ClothingUpdate
        public ActionResult PriceList()
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
            return View(NewClothingBs.ListAll());
        }

        // GET: Admin/ClothingUpdate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/ClothingUpdate/Create
        public ActionResult Create()
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

        // POST: Admin/ClothingUpdate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection)
        {

            //try
            //{
            if (ModelState.IsValid)
            {
                // TODO: Add insert logic here
                ClothingObj.ClothId = collection["ClothId"];
                ClothingObj.ClothDesc = collection["ClothDesc"];
                ClothingObj.Amount = Convert.ToDouble(collection["Amount"]);
                ClothingObj.UserId = "admin";
                ClothingObj.Keydate = DateTime.Now;
                string flag = collection["Flag"];
                if (flag == "false")
                {
                    ClothingObj.Flag = "A";
                    NewClothingBs.Insert(ClothingObj);
                    ViewData["Message"] = "Record save successfully";
                }
                else
                {
                    ClothingObj.Flag = "C";
                    NewClothingBs.Update(ClothingObj);
                    ViewData["Message"] = "Record updated successfully";
                }
               
                //return View();

                // return RedirectToAction("Index");
            }
            return View();
            //}
            //catch
            //{
            //    return View();
            //}
        }

        public JsonResult Search(string id, string key)
        {
            var result = NewClothingBs.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
