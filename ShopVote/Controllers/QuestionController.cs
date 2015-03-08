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
using ShopVote.Filterprofiles;
using ShopVote.Models;

namespace ShopVote.Controllers
{
    public class QuestionController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index()
        {
            //code for the saving question data will go here
            return RedirectToAction("Index", "Home");
        }
    }
}
