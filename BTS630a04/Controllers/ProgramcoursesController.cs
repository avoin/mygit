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
    public class ProgramcoursesController : Controller
    {
        private StaffingContext db = new StaffingContext();

        //
        // GET: /Programcourses/

        public ActionResult Index()
        {
            var programcourses = db.Programcourses.Include(p => p.Program).Include(p => p.Course);
            return View(programcourses.ToList());
        }

        //
        // GET: /Programcourses/Details/5

        public ActionResult Details(int id = 0)
        {
            Programcourses programcourses = db.Programcourses.Find(id);
            if (programcourses == null)
            {
                return HttpNotFound();
            }
            return View(programcourses);
        }

        //
        // GET: /Programcourses/Create

        public ActionResult Create()
        {
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name");
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name");
            return View();
        }

        //
        // POST: /Programcourses/Create

        [HttpPost]
        public ActionResult Create(Programcourses programcourses)
        {
            if (ModelState.IsValid)
            {
                db.Programcourses.Add(programcourses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name", programcourses.ProgramID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", programcourses.CourseID);
            return View(programcourses);
        }

        //
        // GET: /Programcourses/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Programcourses programcourses = db.Programcourses.Find(id);
            if (programcourses == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name", programcourses.ProgramID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", programcourses.CourseID);
            return View(programcourses);
        }

        //
        // POST: /Programcourses/Edit/5

        [HttpPost]
        public ActionResult Edit(Programcourses programcourses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programcourses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name", programcourses.ProgramID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", programcourses.CourseID);
            return View(programcourses);
        }

        //
        // GET: /Programcourses/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Programcourses programcourses = db.Programcourses.Find(id);
            if (programcourses == null)
            {
                return HttpNotFound();
            }
            return View(programcourses);
        }

        //
        // POST: /Programcourses/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Programcourses programcourses = db.Programcourses.Find(id);
            db.Programcourses.Remove(programcourses);
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