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
using System.Data.Entity.Core.Objects;
using System.Web.Routing;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: Blog
        public ActionResult Index(DateTime? date, string postTitle, string postAuthor, string words, int? comments)
        {
            var TitleList = new List<string>();
            var TitleQry = from p in db.Posts orderby p.Title select p.Title;
            TitleList.AddRange(TitleQry.Distinct());
            ViewBag.postTitle = new SelectList(TitleList);

            var AuthorList = new List<string>();
            var AuthorQry = from p in db.Posts orderby p.Name select p.Name;
            AuthorList.AddRange(AuthorQry.Distinct());
            ViewBag.postAuthor = new SelectList(AuthorList);

            var posts = from p in db.Posts select p;
            if (!(date == DateTime.MinValue) && !(date == null))
            {
                posts = posts.Where(x => (x.Date.Day == date.Value.Day) && (x.Date.Month == date.Value.Month) && (x.Date.Year == date.Value.Year));
            }
            if (!String.IsNullOrEmpty(postTitle))
            {
                posts = posts.Where(x => x.Title == postTitle);
            }
            if (!String.IsNullOrEmpty(postAuthor))
            {
                posts = posts.Where(x => x.Name == postAuthor);
            }
            if (!String.IsNullOrEmpty(words))
            {
                posts = posts.Where(x => (x.Content.Contains(words)) || (x.Title.Contains(words)));
            }
            if (!(comments == null))
            {
                posts = posts.Where(x => x.Comments.Count() >= comments);
            }
            

            return View(posts);
        }


        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                ViewBag.ErrorMsg = "No ID";
                return View("Error");
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                ViewBag.ErrorMsg = "Error with details";
                return View("Error");
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
                ViewBag.ErrorMsg = "No ID";
                return View("Error");
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                ViewBag.ErrorMsg = "Error with edit";
                return View("Error");
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

        // GET: Blog/Comments/5
        public ActionResult Comments(int? id)
        {
            return RedirectToAction("index", new RouteValueDictionary(new { controller = "Comments", action = "index", Id = id }));
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                ViewBag.ErrorMsg = "No ID";
                return View("Error");
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                ViewBag.ErrorMsg = "Error with delete";
                return View("Error");
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
