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
    [Authorize(Roles = "Chair, Support")]
    public class TeachingController : Controller
    {
        private StaffingContext db = new StaffingContext();

        //
        // GET: /Teaching/

        public ActionResult Index()
        {
            var teachings = db.Teachings.Include(t => t.Professor).Include(t => t.Course);
            return View(teachings.ToList());
        }

        //
        // GET: /Teaching/Details/5

        public ActionResult Details(int id = 0)
        {
            Teaching teaching = db.Teachings.Find(id);
            if (teaching == null)
            {
                return HttpNotFound();
            }
            return View(teaching);
        }

        //
        // GET: /Teaching/Create

        public ActionResult Create()
        {
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName");
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name");
            return View();
        }

        //
        // POST: /Teaching/Create

        [HttpPost]
        public ActionResult Create(Teaching teaching)
        {
            if (ModelState.IsValid)
            {
                db.Teachings.Add(teaching);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName", teaching.ProfessorID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", teaching.CourseID);
            return View(teaching);
        }

        //
        // GET: /Teaching/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Teaching teaching = db.Teachings.Find(id);
            if (teaching == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName", teaching.ProfessorID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", teaching.CourseID);
            return View(teaching);
        }

        //
        // POST: /Teaching/Edit/5

        [HttpPost]
        public ActionResult Edit(Teaching teaching)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teaching).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName", teaching.ProfessorID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", teaching.CourseID);
            return View(teaching);
        }

        //
        // GET: /Teaching/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Teaching teaching = db.Teachings.Find(id);
            if (teaching == null)
            {
                return HttpNotFound();
            }
            return View(teaching);
        }

        //
        // POST: /Teaching/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Teaching teaching = db.Teachings.Find(id);
            db.Teachings.Remove(teaching);
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