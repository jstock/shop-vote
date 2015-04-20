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
        private FilterCategoriesContext ct = new FilterCategoriesContext();
        private FiltersContext fc = new FiltersContext();

        //
        // GET: /Admin/

        public ActionResult Index()
        {
          return View();
        }

        public ActionResult Category()
        {
          return View(ct.FilterCategories.ToList());
        }

        //
        // GET: /Category/Details/5

        public ActionResult CategoryDetails(int id = 0)
        {
          FilterCategory categ = ct.FilterCategories.Find(id);
          if (categ == null)
          {
            return HttpNotFound();
          }
          return View(categ);
        }

        ////
        //// GET: /category/Create

        //public ActionResult CategoryCreate()
        //{
        //    ViewBag.CategoryId = new SelectList(ct.FilterCategories, "Id", "Name");
        //    return View();
        //}

        ////
        //// POST: /ProductFilter/Create

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CategoryCreate(FilterCategory categ)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ct.FilterCategories.Add(categ);
        //        ct.SaveChanges();
        //        return RedirectToAction("Categ");
        //    }

        //    ViewBag.CategoryId = new SelectList(ct.FilterCategories, "Id", "Name", productfilter.CategoryId);
        //    return View(productfilter);
        //}

        ////
        //// GET: /cetagory/Edit/#

        //public ActionResult CategoryEdit(int id = 0)
        //{
        //    FilterCategory categ = ct.FilterCategories.Find(id);
        //    if (productfilter == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CategoryId = new SelectList(ct.FilterCategories, "Id", "Name", productfilter.CategoryId);
        //    return View(productfilter);
        //}

        ////
        //// POST: /ProductFilter/Edit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CategoryEdit(Category categ)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ct.Entry(categ).State = EntityState.Modified;
        //        ct.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CategoryId = new SelectList(db.FilterCategories, "Id", "Name", productfilter.CategoryId);
        //    return View(categ);
        //}

        ////
        //// GET: /category/Delete/5

        //public ActionResult CategoryDelete(int id = 0)
        //{
        //    FilterCategory categ = ct.FilterCategories.Find(id);
        //    if (categ == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(categ);
        //}

        ////
        //// POST: /category/Delete/#

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult CategoryDeleteConfirmed(int id)
        //{
        //    FilterCategory categ = ct.FilterCategories.Find(id);
        //    ct.FilterCategories.Remove(categ);
        //    ct.SaveChanges();
        //    return RedirectToAction("Category");
        //}

        public ActionResult Filters() 
        {
          var filters = fc.Filters.Where(f => f.Id > 0);
          return View(filters.ToList());
        }

        public ActionResult Products()
        {
            var products = db.Products.Include(p => p.Manufacturer);
            return View(products.ToList());
        }

        public ActionResult Manufacturers() 
        {
            var manufacturers = mb.Manufacturers.Where(m => m.Id > 0).OrderBy(m => m.Name);
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
        // GET: /Admin/GetCategories

        public JsonResult GetCategories()
        {
          return Json(ct.FilterCategories.ToList(), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Admin/GetManufacturers

        public JsonResult GetProducts()
        {
          return Json(db.Products.ToList(), JsonRequestBehavior.AllowGet);
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
