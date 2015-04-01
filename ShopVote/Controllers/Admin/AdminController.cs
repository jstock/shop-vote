using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopVote.Models;

namespace ShopVote.Controllers
{
    public class AdminController : Controller
    {
        private ProductContext db = new ProductContext();

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category()
        {
            return View();
        }

        public ActionResult Product()
        {
          if (ModelState.IsValid) {
            var products = db.Products.Where(p => p.Id > 0);
            return View(products.ToList());
          } else {
            throw new Exception("Error loading page.");
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
