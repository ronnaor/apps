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
        public ActionResult Index(int? id, DateTime? date, string commentTitle, string commentAuthor, string words)
        {
            if (id == null)
            {
                ViewBag.ErrorMsg = "No ID";
                return View("Error");
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                ViewBag.ErrorMsg = "No post";
                return View("Error");
            }

            ViewBag.postName = post.Title;
            ViewBag.commentsCount = post.Comments.Count();
            var comments = db.Comments.Where(c => c.Post.PostID == id);

            var TitleList = new List<string>();
            var TitleQry = from p in comments orderby p.Title select p.Title;
            TitleList.AddRange(TitleQry.Distinct());
            ViewBag.commentTitle = new SelectList(TitleList);

            var AuthorList = new List<string>();
            var AuthorQry = from p in comments orderby p.Name select p.Name;
            AuthorList.AddRange(AuthorQry.Distinct());
            ViewBag.commentAuthor = new SelectList(AuthorList);
          
            if (!(date == DateTime.MinValue) && !(date == null))
            {
                comments = comments.Where(x => (x.Date.Day == date.Value.Day) && (x.Date.Month == date.Value.Month) && (x.Date.Year == date.Value.Year));
            }
            if (!String.IsNullOrEmpty(commentTitle))
            {
                comments = comments.Where(x => x.Title == commentTitle);
            }
            if (!String.IsNullOrEmpty(commentAuthor))
            {
                comments = comments.Where(x => x.Name == commentAuthor);
            }
            if (!String.IsNullOrEmpty(words))
            {
                comments = comments.Where(x => (x.Content.Contains(words)) || (x.Title.Contains(words)));
            }


            return View(comments);
        }

     
        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                ViewBag.ErrorMsg = "No ID";
                return View("Error");
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                ViewBag.ErrorMsg = "Error with edit";
                return View("Error");
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentID,PostID,Title,Name,Web,Content,Date")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index", new RouteValueDictionary(new { controller = "Comments", action = "index", Id = comment.PostID }));
            }
            return View(comment);
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                ViewBag.ErrorMsg = "No ID";
                return View("Error");
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                ViewBag.ErrorMsg = "Error with details";
                return View("Error");
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                ViewBag.ErrorMsg = "No ID";
                return View("Error");
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                ViewBag.ErrorMsg = "Error with delete";
                return View("Error");
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
