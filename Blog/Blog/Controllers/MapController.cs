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
    public class MapController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: Map
        public ActionResult Index(string addressName, double? latNum, double? lngNum)
        {
            var addressList = new List<string>();
            var addressQry = from p in db.Map orderby p.Address select p.Address;
            addressList.AddRange(addressQry.Distinct());
            ViewBag.addressName = new SelectList(addressList);

            var LatList = new List<double>();
            var LatQry = from p in db.Map orderby p.Lat select p.Lat;
            LatList.AddRange(LatQry.Distinct());
            ViewBag.latNum = new SelectList(LatList);

            var LngList = new List<double>();
            var LngQry = from p in db.Map orderby p.Lng select p.Lng;
            LngList.AddRange(LngQry.Distinct());
            ViewBag.lngNum = new SelectList(LngList);

            var addresses = from p in db.Map select p;

            if (!String.IsNullOrEmpty(addressName))
            {
                addresses = addresses.Where(x => x.Address == addressName);
            }
            if (!(latNum == null))
            {
                addresses = addresses.Where(x => x.Lat == latNum);
            }
            if (!(lngNum == null))
            {
                addresses = addresses.Where(x => x.Lng == lngNum);
            }

            return View(addresses);
        }

        // GET: Map/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                ViewBag.ErrorMsg = "No ID";
                return View("Error");
            }
            Map map = db.Map.Find(id);
            if (map == null)
            {
                ViewBag.ErrorMsg = "Error with details";
                return View("Error");
            }
            return View(map);
        }

        // GET: Map/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Map/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Address,Lat,Lng")] Map map)
        {
            if (ModelState.IsValid)
            {
                db.Map.Add(map);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(map);
        }

        // GET: Map/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.ErrorMsg = "No ID";
                return View("Error");
            }
            Map map = db.Map.Find(id);
            if (map == null)
            {
                ViewBag.ErrorMsg = "Error with edit";
                return View("Error");
            }
            return View(map);
        }

        // POST: Map/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Address,Lat,Lng")] Map map)
        {
            if (ModelState.IsValid)
            {
                db.Entry(map).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(map);
        }

        // GET: Map/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                ViewBag.ErrorMsg = "No ID";
                return View("Error");
            }
            Map map = db.Map.Find(id);
            if (map == null)
            {
                ViewBag.ErrorMsg = "Error with delete";
                return View("Error");
            }
            return View(map);
        }


        public ActionResult Locations()
        {
            var locations = db.Map.Select(x => new { x.Lat, x.Lng });
            return View(Json(locations.ToList()));
        }

        // POST: Map/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Map map = db.Map.Find(id);
            db.Map.Remove(map);
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

