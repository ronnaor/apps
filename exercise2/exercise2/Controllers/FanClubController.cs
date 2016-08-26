using exercise2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace exercise2.Controllers
{
    public class FanClubController : Controller
    {
        static List<Fan> fans = new List<Fan>();

        // GET: FanClub
        public ActionResult Index()
        {
            return View(fans);
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

        // GET:  FanClub/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST:  FanClub/Create
        [HttpPost]
        public ActionResult Create(Fan fan)
        {
            if (ModelState.IsValid)
            {
                fans.Add(fan);
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
            if (!id.HasValue)
            {
                TempData["ErrorMsg"] = "No ID";
                return RedirectToAction("Error");
            }
            else if (!fans.Exists(x => x.ID == id.Value))
            {
                TempData["ErrorMsg"] = "Wrong ID sent";
                return RedirectToAction("Error");
            }

            return View(fans[fans.FindIndex(w => w.ID == id.Value)]);
        }

        // GET: FanClub/Edit
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMsg"] = "No ID";
                return RedirectToAction("Error");
            }
            foreach (Fan fan in fans)
            {
                if (fan.ID.Equals(id))
                {
                    return View(fan);
                }
            }
            TempData["ErrorMsg"] = "Error with edit";
            return RedirectToAction("Error");
        }

        // POST: FanClub/Edit
        [HttpPost]
        public ActionResult Edit(int? id, Fan fan)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMsg"] = "No ID";
                return RedirectToAction("Error");
            }
            if (ModelState.IsValid)
            {
                foreach (Fan f in fans)
                {
                    if (f.ID.Equals(id))
                    {
                        f.copy(fan);
                        return RedirectToAction("Index");
                    }
                }
                TempData["ErrorMsg"] = "Error with edit";
                return RedirectToAction("Error");
            }
            return View();
        }

        // GET: First/Delete
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                TempData["ErrorMsg"] = "No ID";
                return RedirectToAction("Error");
            }
            foreach (Fan fan in fans)
            {
                if (fan.ID.Equals(id))
                {
                    fans.RemoveAt(fans.FindIndex(w => w.ID == id.Value));
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Error");

        }
    }
}
