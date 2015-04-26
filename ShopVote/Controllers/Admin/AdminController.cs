using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Web.Security;
using ShopVote.Models;

namespace ShopVote.Controllers.Admin
{
  public class AdminController : Controller
  {
    private ProductContext db = new ProductContext();
    private ManufacturersContext mb = new ManufacturersContext();
    private FeedbackContext fb = new FeedbackContext();
    private FilterCategoriesContext ct = new FilterCategoriesContext();
    private FiltersContext fc = new FiltersContext();
    private FilterProfilesContext fp = new FilterProfilesContext();
    private UsersContext uc = new UsersContext();

    //
    // GET: /Admin/

    public ActionResult Index()
    {
      var feedbacks = fb.Feedbacks.Where(f => f.Id >= 0);
      return View(feedbacks.ToList());
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
    // GET: /Admin/GetManufacturers

    public JsonResult GetManufacturers()
    {
      return Json(mb.Manufacturers.ToList(), JsonRequestBehavior.AllowGet);
    }

    //
    // GET: /Admin/GetCategories

    public JsonResult GetCategories()
    {
      return Json(ct.FilterCategories.ToList(), JsonRequestBehavior.AllowGet);
    }

    //
    // GET: /Admin/GetQuestions

    public JsonResult GetQuestions()
    {
      return Json(fp.Questions.ToList(), JsonRequestBehavior.AllowGet);
    }

    //
    // GET: /Admin/GetProducts

    public JsonResult GetProducts()
    {
      Product prod = new Product();
      prod.Id = 0;
      prod.Name = "";
      prod.ManufacturerID = 0;
      List<Product> prods = new List<ShopVote.Models.Product>();
      prods.Add(prod);
      prods.AddRange(db.Products.ToList());

      return Json(prods, JsonRequestBehavior.AllowGet);
    }

    public ActionResult CreateFilter()
    {
      Product prod = new Product();
      prod.Id = 0;
      prod.Name = "";
      prod.ManufacturerID = 0;
      ViewBag.Products = new List<ShopVote.Models.Product>();
      ViewBag.Products.Add(prod);
      ViewBag.Products.AddRange(db.Products.Where(p => p.Id > 0).OrderBy(p => p.Name).ToList());
      ViewBag.Manufacturers = db.Manufacturers.Where(m => m.Id > 0).OrderBy(p => p.Name).ToList();
      ViewBag.Categories = ct.FilterCategories.Where(fc => fc.Id > 0).ToList();
      ViewBag.Questions = fp.Questions.Where(q => q.Id > 0).ToList();
      return View();
    }

    //
    // POST: /Admin/CreateFilter

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult CreateFilter(PMFilter filter)
    {
      if (ModelState.IsValid && filter != null)
      {
        filter.Product = db.Products.Where(p => p.Id == filter.ProductID).FirstOrDefault();
        fc.Filters.Add(filter);
        fc.SaveChanges();
      }

      return RedirectToAction("filters", "admin");
    }

    //
    // POST: /Admin/EditFilter

    [HttpPost]
    public JsonResult EditFilter(PMFilter filter)
    {

      if (filter == null)
      {
        return Json("filter cannot be null", JsonRequestBehavior.AllowGet);
      }

      var old = fc.Filters.Where(f => f.Id == filter.Id).FirstOrDefault();
      if (old == null)
      {
        return Json("filter not found", JsonRequestBehavior.AllowGet);
      }

      var cat = ct.FilterCategories.Where(c => c.Id == filter.CategoryID).FirstOrDefault();
      if (cat == null)
      {
        return Json("category not found", JsonRequestBehavior.AllowGet);
      }

      var quest = fp.Questions.Where(q => q.Id == filter.QuestionID).FirstOrDefault();
      if (cat == null)
      {
        return Json("question not found", JsonRequestBehavior.AllowGet);
      }

      var man = mb.Manufacturers.Where(m => m.Id == filter.ManufacturerID).FirstOrDefault();
      if (man == null)
      {
        return Json("manufacturer not found", JsonRequestBehavior.AllowGet);
      }

      var prod = db.Products.Where(m => m.Id == filter.ProductID).FirstOrDefault();
      if (man == null)
      {
        return Json("product not found", JsonRequestBehavior.AllowGet);
      }

      old.CategoryID = filter.CategoryID;
      old.ManufacturerID = filter.ManufacturerID;
      old.QuestionID = filter.QuestionID;
      old.ProductID = filter.ProductID;
      old.FilterValue = filter.FilterValue;

      int result = fc.SaveChanges();
      if (result <= 0)
      {
        return Json("transaction error", JsonRequestBehavior.AllowGet);
      }

      return Json("edited successfully", JsonRequestBehavior.AllowGet);
    }

    //
    // POST: /Admin/DeleteFilter

    [HttpPost]
    public JsonResult DeleteFilter(string ID)
    {

      var filterID = Convert.ToInt32(ID);
      if (filterID <= 0)
      {
        return Json("filterID must be greater than 0", JsonRequestBehavior.AllowGet);
      }

      var filter = fc.Filters.Where(f => f.Id == filterID).FirstOrDefault();
      if (filter == null)
      {
        return Json("filter not found", JsonRequestBehavior.AllowGet);
      }

      try
      {

        fc.Filters.Remove(filter);
        fc.SaveChanges();
        return Json("filter deleted successfully", JsonRequestBehavior.AllowGet);

      }
      catch (DataException)
      {
        return Json("transaction error", JsonRequestBehavior.AllowGet);
      }

    }

    //
    // POST: /Admin/EditProduct

    [HttpPost]
    public JsonResult EditProduct(Product product)
    {

      if (product == null)
      {
        return Json("product cannot be null", JsonRequestBehavior.AllowGet);
      }

      var old = db.Products.Where(p => p.Id == product.Id).FirstOrDefault();
      if (old == null)
      {
        return Json("product not found", JsonRequestBehavior.AllowGet);
      }

      var man = db.Manufacturers.Where(m => m.Id == product.ManufacturerID).FirstOrDefault();
      if (man == null)
      {
        return Json("manufacturer not found", JsonRequestBehavior.AllowGet);
      }

      old.Name = product.Name;
      old.ManufacturerID = product.ManufacturerID;
      old.Manufacturer = man;
      old.Description = product.Description;

      int result = db.SaveChanges();
      if (result <= 0)
      {
        return Json("transaction error", JsonRequestBehavior.AllowGet);
      }

      return Json("edited successfully", JsonRequestBehavior.AllowGet);
    }

    //
    // POST: /Admin/DeleteProduct

    [HttpPost]
    public JsonResult DeleteProduct(string ID)
    {

      var productID = Convert.ToInt32(ID);
      if (productID <= 0)
      {
        return Json("productID must be greater than 0", JsonRequestBehavior.AllowGet);
      }

      var prod = db.Products.Where(p => p.Id == productID).FirstOrDefault();

      if (prod == null)
      {
        return Json("product not found", JsonRequestBehavior.AllowGet);
      }

      try
      {

        db.Products.Remove(prod);
        db.SaveChanges();
        return Json("product deleted successfully", JsonRequestBehavior.AllowGet);

      }
      catch (DataException)
      {
        return Json("transaction error", JsonRequestBehavior.AllowGet);
      }

    }

    public ActionResult Company()
    {
      return View();
    }

    public ActionResult ManageUser()
    {
      return View(uc.UserProfiles.Where(a => a.UserId > 0).ToList());
    }

    public ActionResult ManageAdmin()
    {
      var admins = uc.UserProfiles.Where(a => a.UserId > 0).ToList();
      admins = admins.Where(a => a.IsInRole("Admin")).ToList();
      return View(admins);
    }

    public ActionResult AddAdmin()
    {
      return View(uc.UserProfiles.Where(a => a.UserId > 0).ToList());
    }

    [HttpPost]
    public JsonResult AddAdmin(UserProfile newAdmin)
    {
      UserProfile user = uc.UserProfiles.Where(u => u.UserId == newAdmin.UserId).FirstOrDefault();
      if (user == null)
      {
        return Json("user not found", JsonRequestBehavior.AllowGet);
      }
      
      // TODO: add user to admin role with MySqlRoleProvider
      return Json("admin added successfully", JsonRequestBehavior.AllowGet);
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
