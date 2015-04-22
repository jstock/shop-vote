using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using ShopVote.Filters;
using ShopVote.Models;

namespace ShopVote.Controllers.Admin
{
    public class ShoppingListController : Controller
    {
        private ShoppingListContext db = new ShoppingListContext();
        private ProductContext dp = new ProductContext();

        //
        // GET: /ShoppingList/

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        // POST: /ShoppingList/Create
        [HttpPost]
        public JsonResult Create(ShoppingList List)
        {
            int id = WebSecurity.CurrentUserId;
            List.UserId = id;
            db.ShoppingList.Add(List);
            int result = db.SaveChanges();

            if (result <= 0)
            {
                return Json("transaction error", JsonRequestBehavior.AllowGet);
            }

            return Json("created successfully", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Display(ShoppingList model)
        {

            int id = WebSecurity.CurrentUserId;

            if (ModelState.IsValid && model != null)
            {
                var shoppingLists = db.ShoppingList.Where(p => p.UserId == id);
                if (shoppingLists.ToList().Count > 0)
                {
                    return View(shoppingLists.ToList());
                }
            }
            return View(new List<ShoppingList>());
        }
        // GET: /ShoppingList/Create
        public ActionResult Create()
        {
            return HttpNotFound();
        }
        public ActionResult Delete(int id)
        {
            ShoppingList list = db.ShoppingList.Find(id);
            db.ShoppingList.Remove(list);
            db.SaveChanges();
            return RedirectToAction("Display");
        }
        public ActionResult ViewList(int id)
        {
            List<Product> output = new List<Product>();
            var result = (from x in db.ShoppingListProducts where x.ShoppingListId == id select x).ToArray();
            foreach (var thing in result)
            {
                var item = (from y in dp.Products where y.Id == thing.ProductId select y).ToList();
                output.AddRange(item);
            }
            //var products = db.ShoppingListProducts.Where(p=> p.ShoppingListId == id);
            return View(output);
        }

        public ActionResult SelectList()
        {
            int id = WebSecurity.CurrentUserId;
            var list = db.ShoppingList.Where(p => p.UserId == id);
            return View(list);
        }
        public ActionResult Add(int id)
        {
            ShoppingListProducts element = new ShoppingListProducts();
            element.ShoppingListId = id;
            element.ProductId = (int)Session["productId"];
            db.ShoppingListProducts.Add(element);
            db.SaveChanges();
            return RedirectToAction("Index", "Products");
        }

    }
}