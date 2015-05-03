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
    public class ProductsController : Controller
    {
        private ProductContext db = new ProductContext();
        private ShoppingListContext dl = new ShoppingListContext();
        private FilterProfilesContext fpc = new FilterProfilesContext();
        private UsersContext uc = new UsersContext();

        //
        // GET: /Products/GetProfile

        public JsonResult GetProfile()
        {
          var usr = uc.UserProfiles.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
          var profile = fpc.FilterProfiles.Where(fp => fp.UserID == usr.UserId);
          return Json(profile.ToList(), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Product/

        public ActionResult Index(string searchText = "")
        {
            var products = db.Products.Where(p => p.Id > 0).Include(p => p.Manufacturer);
            
            if (!String.IsNullOrWhiteSpace(searchText)) {
              products = products.Where(p => p.Name.Contains(searchText) || p.Description.Contains(searchText));
            } 

            return View(products.ToList());
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "Id", "Name");
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Product", "Admin");
            }

            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "Id", "Name", product.ManufacturerID);
            return View(product);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "Id", "Name", product.ManufacturerID);
            return View(product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManufacturerID = new SelectList(db.Manufacturers, "Id", "Name", product.ManufacturerID);
            return View(product);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
       
        public ActionResult AddToList(int id)
        {
            Session["productId"] = id;
            return RedirectToAction("SelectList", "ShoppingList");
        }
    }
}