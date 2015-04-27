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
    private UsersContext uc = new UsersContext();

    //
    // GET: /Questions/

    public ActionResult Index()
    {
      var categories = fc.FilterCategories.Where(c => c.Id > 0).ToList();
      var questions = fp.Questions.Where(q => q.Id > 0).ToList();
      return View(Tuple.Create(categories, questions));
    }

    [HttpPost]
    public JsonResult SaveProfile(FPValues values)
    {
      var user = uc.UserProfiles.Where(u => u.UserName == HttpContext.User.Identity.Name).FirstOrDefault();
      int uid = user.UserId;

      for (int i = 0; i < values.Questions.Length; i++)
      {
          FilterProfile entry = new FilterProfile();
          entry.UserID = uid;
          entry.CategoryID = int.Parse(values.Categories[i]);
          entry.QuestionID = int.Parse(values.Questions[i]);
          entry.Value = decimal.Parse(values.Values[i]);
          fp.FilterProfiles.Add(entry);
      }

      int result = fp.SaveChanges();
      if (result <= 0) { return Json("transaction failed", JsonRequestBehavior.AllowGet); }

      return Json("success", JsonRequestBehavior.AllowGet);
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
