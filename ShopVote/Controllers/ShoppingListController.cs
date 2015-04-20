﻿using System;
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
            if (ModelState.IsValid)
            {
                var shoppingLists = db.ShoppingList.Where(p => p.UserId.Equals(User.Identity));
                if (shoppingLists.ToList().Count > 0)
                {
                    return View(shoppingLists.ToList());
                }
            }
            return View();
        }
        // GET: /ShoppingList/Create
        public ActionResult Create()
        {
            return HttpNotFound();
        }

    }
}