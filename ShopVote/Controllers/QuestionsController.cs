using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopVote.Models;

namespace ShopVote.Controllers
{
    public class QuestionsController : Controller
    {
        private FilterCategoriesContext fc = new FilterCategoriesContext();
        private FilterProfilesContext fp = new FilterProfilesContext();

        //
        // GET: /Questions/

        public ActionResult Index()
        {
            var categories = fc.FilterCategories.Where(c => c.Id > 0).ToList();
            var questions = fp.Questions.Where(q => q.Id > 0).ToList();
            return View(Tuple.Create(categories, questions));
        }

        //
        // GET: /Questions/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Questions/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Questions/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Questions/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Questions/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Questions/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Questions/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
