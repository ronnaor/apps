using Blog.DAL;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Controllers
{
    public class AccountController : Controller
    {
        private BlogDBContext db = new BlogDBContext();
        // GET: Account
        public ActionResult Index(string fName, string lName, string usrName)
        {
            var FirstNameList = new List<string>();
            var FirstNameQry = from p in db.Manager orderby p.Name select p.Name;
            FirstNameList.AddRange(FirstNameQry.Distinct());
            ViewBag.fName = new SelectList(FirstNameList);

            var LastNameList = new List<string>();
            var LastNameQry = from p in db.Manager orderby p.LastName select p.LastName;
            LastNameList.AddRange(LastNameQry.Distinct());
            ViewBag.lName = new SelectList(LastNameList);

            var usrNameList = new List<string>();
            var usrNameQry = from p in db.Manager orderby p.UserName select p.UserName;
            usrNameList.AddRange(usrNameQry.Distinct());
            ViewBag.usrName = new SelectList(usrNameList);

            var managers = from p in db.Manager select p;
           
            if (!String.IsNullOrEmpty(fName))
            {
                managers = managers.Where(x => x.Name == fName);
            }
            if (!String.IsNullOrEmpty(lName))
            {
                managers = managers.Where(x => x.LastName == lName);
            }
            if (!String.IsNullOrEmpty(usrName))
            {
                managers = managers.Where(x => x.UserName == usrName);
            }


            return View(managers);
        }

        //GET: Register
        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "ID,Name,LastName,UserName,Password,confirmPassword")] Manager account)
        {
            var v = from p in db.Manager where p.UserName == account.UserName select p;
            if ((ModelState.IsValid) && (!v.Any()))
            {
                db.Manager.Add(account);
                db.SaveChanges();

                ModelState.Clear();
                Session["ID"] = account.ID.ToString();
                Session["UserName"] = account.UserName.ToString();
                Session["Name"] = account.Name.ToString();
                return RedirectToAction("LoggedIn");
               
            }
            if (v.Any())
            {
                ViewBag.usrcpy = "**This user name is already in use.**";
            }
            return View(account);
        }

        //GET: Login
        public ActionResult Login()
        {
            return View();
        }

        //POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Manager user)
        {
            var usr = db.Manager.Where(u => u.UserName == user.UserName && u.Password == user.Password).FirstOrDefault();
            if (usr != null)
            {
                Session["ID"] = usr.ID.ToString();
                Session["UserName"] = usr.UserName.ToString();
                Session["Name"] = usr.Name.ToString();
                return RedirectToAction("LoggedIn");
            }
            else
            {
                ViewBag.ErrorMsg = ("UserName or Password are not correct");
            }
            return View();
        }

        //LoggedIn
        public ActionResult LoggedIn()
        {
            if(Session["ID"] != null)
            {
                return RedirectToAction("index", new RouteValueDictionary(new { controller = "BlogGuests", action = "index" }));

            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //LoggedOut
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("index", new RouteValueDictionary(new { controller = "BlogGuests", action = "index"}));

        }


        // GET: Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                ViewBag.ErrorMsg = "No ID";
                return View("Error");
            }
            Manager manager = db.Manager.Find(id);
            if (manager == null)
            {
                ViewBag.ErrorMsg = "Error with details";
                return View("Error");
            }
            return View(manager);
        }

        // GET: Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                ViewBag.ErrorMsg = "No ID";
                return View("Error");
            }
            Manager manager = db.Manager.Find(id);
            if (manager == null)
            {
                ViewBag.ErrorMsg = "Error with edit";
                return View("Error");
            }
            return View(manager);
        }

        // POST: Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,LastName,UserName,Password,confirmPassword")] Manager account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                Session["Name"] = account.Name.ToString();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                ViewBag.ErrorMsg = "No ID";
                return View("Error");
            }
            Manager manager = db.Manager.Find(id);
            if (manager == null)
            {
                ViewBag.ErrorMsg = "Error with delete";
                return View("Error");
            }
            return View(manager);
        }

        // POST: Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manager manager = db.Manager.Find(id);
            db.Manager.Remove(manager);
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

