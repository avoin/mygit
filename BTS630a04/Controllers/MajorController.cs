using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTS630a04.Models;

namespace BTS630a04.Controllers
{
    public class MajorController : Controller
    {
        private StaffingContext db = new StaffingContext();

        //
        // GET: /Major/

        public ActionResult Index()
        {
            return View(db.Majors.ToList());
        }

        //
        // GET: /Major/Details/5

        public ActionResult Details(int id = 0)
        {
            Major major = db.Majors.Find(id);
            if (major == null)
            {
                return HttpNotFound();
            }
            return View(major);
        }

        //
        // GET: /Major/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Major/Create

        [HttpPost]
        public ActionResult Create(Major major)
        {
            if (ModelState.IsValid)
            {
                db.Majors.Add(major);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(major);
        }

        //
        // GET: /Major/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Major major = db.Majors.Find(id);
            if (major == null)
            {
                return HttpNotFound();
            }
            return View(major);
        }

        //
        // POST: /Major/Edit/5

        [HttpPost]
        public ActionResult Edit(Major major)
        {
            if (ModelState.IsValid)
            {
                db.Entry(major).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(major);
        }

        //
        // GET: /Major/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Major major = db.Majors.Find(id);
            if (major == null)
            {
                return HttpNotFound();
            }
            return View(major);
        }

        //
        // POST: /Major/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Major major = db.Majors.Find(id);
            db.Majors.Remove(major);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}