using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.DAL;
using Blog.Models;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: Blog
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
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

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMsg"] = "No ID";
                return RedirectToAction("Error");
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                TempData["ErrorMsg"] = "No ID";
                return RedirectToAction("Error");
            }
            return View(post);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostID,Title,Name,Web,Date,Content,Image,Video")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMsg"] = "No ID";
                return RedirectToAction("Error");
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                TempData["ErrorMsg"] = "Error with edit";
                return RedirectToAction("Error");
            }
            return View(post);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostID,Title,Name,Web,Date,Content,Image,Video")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMsg"] = "No ID";
                return RedirectToAction("Error");
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                TempData["ErrorMsg"] = "No Data";
                return RedirectToAction("Error");
            }
            return View(post);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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
