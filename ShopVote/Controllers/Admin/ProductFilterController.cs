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
    public class ProductFilterController : Controller
    {
        private ProductFiltersContext db = new ProductFiltersContext();

        //
        // GET: /ProductFilter/

        public ActionResult Index()
        {
            var filters = db.Filters.Include(p => p.FilterCategory).Include(p => p.Manufacturer).Include(p => p.Product);
            return View(filters.ToList());
        }

        //
        // GET: /ProductFilter/Details/5

        public ActionResult Details(int id = 0)
        {
            ProductFilter productfilter = db.Filters.Find(id);
            if (productfilter == null)
            {
                return HttpNotFound();
            }
            return View(productfilter);
        }

        //
        // GET: /ProductFilter/Create

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.FilterCategories, "Id", "Name");
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            return View();
        }

        //
        // POST: /ProductFilter/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductFilter productfilter)
        {
            if (ModelState.IsValid)
            {
                db.Filters.Add(productfilter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.FilterCategories, "Id", "Name", productfilter.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "Id", "Name", productfilter.ManufacturerId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productfilter.ProductId);
            return View(productfilter);
        }

        //
        // GET: /ProductFilter/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ProductFilter productfilter = db.Filters.Find(id);
            if (productfilter == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.FilterCategories, "Id", "Name", productfilter.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "Id", "Name", productfilter.ManufacturerId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productfilter.ProductId);
            return View(productfilter);
        }

        //
        // POST: /ProductFilter/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductFilter productfilter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productfilter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.FilterCategories, "Id", "Name", productfilter.CategoryId);
            ViewBag.ManufacturerId = new SelectList(db.Manufacturers, "Id", "Name", productfilter.ManufacturerId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", productfilter.ProductId);
            return View(productfilter);
        }

        //
        // GET: /ProductFilter/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ProductFilter productfilter = db.Filters.Find(id);
            if (productfilter == null)
            {
                return HttpNotFound();
            }
            return View(productfilter);
        }

        //
        // POST: /ProductFilter/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductFilter productfilter = db.Filters.Find(id);
            db.Filters.Remove(productfilter);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}