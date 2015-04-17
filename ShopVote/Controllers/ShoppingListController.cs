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
            return View();
        }
        // POST: /ShoppingList/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShoppingList model)
        {
            
                //db.ShoppingList.Add(model);
              //  db.SaveChanges();
                return HttpNotFound();
            

          //  return View();
        }
        public ActionResult Display(ShoppingList model)
        {
            if (ModelState.IsValid)
            {
                var shoppingLists = db.ShoppingList.Where(p => p.ShoppingListId > 0);
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