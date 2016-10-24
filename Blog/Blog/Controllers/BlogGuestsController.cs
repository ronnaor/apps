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
using Blog.ViewModels;

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

        //GET 
        public ActionResult JoinGroup()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JoinGroup(string submit)
        {
            var v = new ViewModel();
            switch (submit)
            {
                case "Fans that are managers":
                    v.FanManager = from manager in db.Manager
                                   join fan in db.Fans on new { manager.Name, manager.LastName } equals new { fan.Name, fan.LastName }
                                   select new FanManager { Name = manager.Name, LastName = manager.LastName, UserName = manager.UserName, BirthDate = fan.BirthDate, Seniority = fan.Seniority };
                    ViewBag.msg = "FanManager";
                    break;
                case "Managers Comments":
                    v.CommManager = from comm in db.Comments
                                    join manager in db.Manager on comm.Name equals manager.UserName
                                    select new CommentManager { FirstName = manager.Name, LastName = manager.LastName, UserName = manager.UserName, PostTitle = comm.Post.Title, CommentTitle = comm.Title, CommentDate = comm.Date };
                    ViewBag.msg = "CommManager";
                    break;
                case "Managers Comments group":
                    var join = from comm in db.Comments
                               join manager in db.Manager on comm.Name equals manager.UserName
                               select new CommentManager { FirstName = manager.Name, LastName = manager.LastName, UserName = manager.UserName, PostTitle = comm.Post.Title, CommentTitle = comm.Title, CommentDate = comm.Date };
                    v.GroupCommManager = join.GroupBy(x => x.PostTitle, (Key, g) => new GroupCommManager { PostTitle = Key, Values = g.ToList() });
                    ViewBag.msg = "GroupCommManager";
                    break;
                case "Group comments by post":
                    v.CommentsGtoup = from comm in db.Comments
                                      group comm by comm.Post.Title into q
                                      select new CommentViewModel { PostTitle = q.Key, Values = q.ToList() };
                    ViewBag.msg = "groupBy";
                    break;
            }

            return View(v);
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
