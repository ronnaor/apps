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
    public class BlogGuestsController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: BlogGuests
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }

        // POST: BlogGuests
        [HttpPost]
        public ActionResult Index(int postID, string title, string name, string website, string comment)
        {
            Comment comm = new Comment
            {
                Title = title,
                Name = name,
                Web = website,
                Content = comment,
                Date = DateTime.Now,
                Post = db.Posts.Find(postID)
            };
            db.Comments.Add(comm);
            db.SaveChanges();
            return View(db.Posts.ToList());
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
