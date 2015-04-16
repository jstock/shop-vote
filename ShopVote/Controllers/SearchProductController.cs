using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopVote.Models;

namespace ShopVote.Controllers
{
    public class SearchProductController : Controller
    {
        private ProductContext db = new ProductContext();
        //
        // GET: /UserProduct/

        public ActionResult Index(string search)
        {
          return view(db.product.where(x=>x.Name.StartsWith(search).ToList))
        }
    }
}
