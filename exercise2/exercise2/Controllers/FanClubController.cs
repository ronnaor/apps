using exercise2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace exercise2.Controllers
{
    public class FanClubController : Controller
    {
        private FanDBContext db = new FanDBContext();

        // GET: FanClub
        public ActionResult Index()
        {
            return View(db.Fans.ToList());
        }

        // GET: FanClub/Error
        public ActionResult Error()
        {
            if (TempData["ErrorMsg"] != null)
            {
                ViewBag.ErrorMsg = TempData["ErrorMsg"];
            }
            return View();
        }

        // GET:  FanClub/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST:  FanClub/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,LastName,Gender,BirthDate,Seniority")] Fan fan)
        {
            if (ModelState.IsValid)
            {
                db.Fans.Add(fan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        // GET: /FanClub/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMsg"] = "No ID";
                return RedirectToAction("Error");
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                TempData["ErrorMsg"] = "No ID";
                return RedirectToAction("Error");
            }
            return View(fan);
        }

        // GET: FanClub/Edit
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMsg"] = "No ID";
                return RedirectToAction("Error");
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                TempData["ErrorMsg"] = "Error with edit";
                return RedirectToAction("Error");
            }
            return View(fan);
            
        }

        // POST: FanClub/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,LastName,Gender,BirthDate,Seniority")] Fan fan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fan);
        }

        // GET: First/Delete
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMsg"] = "No ID";
                return RedirectToAction("Error");
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                TempData["ErrorMsg"] = "No Data";
                return RedirectToAction("Error");
            }
            return View(fan);
        }
        // POST: /Fans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fan fan = db.Fans.Find(id);
            db.Fans.Remove(fan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
