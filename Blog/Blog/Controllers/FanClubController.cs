using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using Blog.DAL;

namespace Blog.Controllers
{
    public class FanClubController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: FanClub
        public ActionResult Index(DateTime? date, string firstName, string lastName, string gender, int? Seniority)
        {
            var FirstNameList = new List<string>();
            var FirstNameQry = from p in db.Fans orderby p.Name select p.Name;
            FirstNameList.AddRange(FirstNameQry.Distinct());
            ViewBag.firstName = new SelectList(FirstNameList);

            var LastNameList = new List<string>();
            var LastNameQry = from p in db.Fans orderby p.LastName select p.LastName;
            LastNameList.AddRange(LastNameQry.Distinct());
            ViewBag.lastName = new SelectList(LastNameList);

            var Gender = new List<string> { "Male" , "Female" };
            ViewBag.gender = new SelectList(Gender);

            var fans = from p in db.Fans select p;
            
            if (!(date == DateTime.MinValue) && !(date == null))
            {
                fans = fans.Where(x => (x.BirthDate.Day == date.Value.Day) && (x.BirthDate.Month == date.Value.Month) && (x.BirthDate.Year == date.Value.Year));
            }
            if (!String.IsNullOrEmpty(firstName))
            {
                fans = fans.Where(x => x.Name == firstName);
            }
            if (!String.IsNullOrEmpty(lastName))
            {
                fans = fans.Where(x => x.LastName == lastName);
            }
            if (!String.IsNullOrEmpty(gender))
            {
                fans = fans.Where(x => x.Gender.ToString() == gender);
            }
            if (!(Seniority == null))
            {
                fans = fans.Where(x => x.Seniority >= Seniority);
            }


            return View(fans);
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
                ViewBag.ErrorMsg = "No ID";
                return View("Error");
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                ViewBag.ErrorMsg = "No Data";
                return View("Error");
            }
            return View(fan);
        }

        // GET: FanClub/Edit
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                ViewBag.ErrorMsg = "No ID";
                return View("Error");
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                ViewBag.ErrorMsg = "Error with edit";
                return View("Error");
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

        // GET: FanClub/Delete
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                ViewBag.ErrorMsg = "No ID";
                return View("Error");
            }
            Fan fan = db.Fans.Find(id);
            if (fan == null)
            {
                ViewBag.ErrorMsg = "No Data";
                return View("Error");
            }
            return View(fan);
        }
        // POST: /FanClub/Delete/5
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