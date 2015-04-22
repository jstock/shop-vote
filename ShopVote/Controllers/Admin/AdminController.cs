using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopVote.Models;

namespace ShopVote.Controllers.Admin
{
    public class AdminController : Controller
    {
        private ProductContext db = new ProductContext();
        private ManufacturersContext mb = new ManufacturersContext();
        private FeedbackContext fb = new FeedbackContext();

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            var feedbacks = fb.Feedbacks.Where(f => f.Id >= 0);
            return View(feedbacks.ToList());
        }

        public ActionResult Category()
        {
            return View();
        }

        public ActionResult Products()
        {
            var products = db.Products.Include(p => p.Manufacturer);
            return View(products.ToList());
        }

        public ActionResult Manufacturers() 
        {
            var manufacturers = db.Manufacturers.Where(m => m.Id > 0).OrderBy(m => m.Name);
            return View(manufacturers.ToList());
        }

        //
        // POST: /Admin/EditProduct

        [HttpPost]
        public JsonResult EditProduct(Product product) {

          if (product == null) {
            return Json("product cannot be null", JsonRequestBehavior.AllowGet);
          }

          var old = db.Products.Where(p => p.Id == product.Id).FirstOrDefault();
          if (old == null) {
            return Json("product not found", JsonRequestBehavior.AllowGet);
          }

          var man = db.Manufacturers.Where(m => m.Id == product.ManufacturerID).FirstOrDefault();
          if (man == null) {
            return Json("manufacturer not found", JsonRequestBehavior.AllowGet);
          }

          old.Name = product.Name;
          old.ManufacturerID = product.ManufacturerID;
          old.Manufacturer = man;
          old.Description = product.Description;

          int result = db.SaveChanges();
          if (result <= 0) {
            return Json("transaction error", JsonRequestBehavior.AllowGet);
          }

          return Json("edited successfully", JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Admin/GetManufacturers

        public JsonResult GetManufacturers() {
          return Json(db.Manufacturers.ToList(), JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Admin/DeleteProduct

        [HttpPost]
        public JsonResult DeleteProduct(string ID) {
          
          var productID = Convert.ToInt32(ID);
          if (productID <= 0) {
            return Json("productID must be greater than 0", JsonRequestBehavior.AllowGet);
          }

          var prod = db.Products.Where(p => p.Id == productID).FirstOrDefault();
          
          if (prod == null) {
            return Json("product not found", JsonRequestBehavior.AllowGet);
          }

          try {

            db.Products.Remove(prod);
            db.SaveChanges();
            return Json("product deleted successfully", JsonRequestBehavior.AllowGet);

          } catch (DataException) {
            return Json("transaction error", JsonRequestBehavior.AllowGet);
          }

        }

        public ActionResult Company()
        {
            return View();
        }

        public ActionResult ManageUser()
        {
            return View();
        }

        public ActionResult ManageAdmin()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Setting()
        {
            return View();
        }
    }
}
