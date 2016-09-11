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
using System.Web.Routing;

namespace Blog.Controllers
{
    public class CommentsController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: Comments
        public ActionResult Index(int? id)
        {
            if (id == null)
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

            ViewBag.postName = post.Title;
            ViewBag.commentsCount = post.Comments.Count();
            var comments = db.Comments.Where(c => c.Post.PostID == id);
            return View(comments.ToList());
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

       
        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMsg"] = "No ID";
                return RedirectToAction("Error");
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                TempData["ErrorMsg"] = "No Data";
                return RedirectToAction("Error");
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("index", new RouteValueDictionary(new { controller = "Comments", action = "index", Id = comment.PostID }));
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
