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
    public class ExperienceController : Controller
    {
        private StaffingContext db = new StaffingContext();

        //
        // GET: /Experience/

        public ActionResult Index()
        {
            return View(db.Experiences.ToList());
        }

        //
        // GET: /Experience/Details/5

        public ActionResult Details(int id = 0)
        {
            Experience experience = db.Experiences.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        //
        // GET: /Experience/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Experience/Create

        [HttpPost]
        public ActionResult Create(Experience experience)
        {
            if (ModelState.IsValid)
            {
                db.Experiences.Add(experience);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(experience);
        }

        //
        // GET: /Experience/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Experience experience = db.Experiences.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        //
        // POST: /Experience/Edit/5

        [HttpPost]
        public ActionResult Edit(Experience experience)
        {
            if (ModelState.IsValid)
            {
                db.Entry(experience).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(experience);
        }

        //
        // GET: /Experience/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Experience experience = db.Experiences.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        //
        // POST: /Experience/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Experience experience = db.Experiences.Find(id);
            db.Experiences.Remove(experience);
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