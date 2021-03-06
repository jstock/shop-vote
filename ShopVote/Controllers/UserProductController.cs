﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopVote.Models;

namespace ShopVote.Controllers
{
    public class UserProductController : Controller
    {
        private ProductContext db = new ProductContext();
        //
        // GET: /UserProduct/

        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Manufacturer);
            return View(products.ToList());

        }
        public ActionResult Details(int id = 0)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

    }
}
