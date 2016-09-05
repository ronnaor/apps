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
        public ActionResult Index(string title, string name, string web, string content)
        {
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
