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
    [Authorize(Roles = "Chair")]
    public class SemestercoursesController : Controller
    {
        private StaffingContext db = new StaffingContext();

        //
        // GET: /Semestercourses/

        public ActionResult Index()
        {
            var semestercourses = db.SemesterCourses.Include(s => s.Semester).Include(s => s.Course);
            //manually match program name to corresponding list entry

            return View(semestercourses.ToList());
        }

        //
        // GET: /Semestercourses/Details/5

        public ActionResult Details(int id = 0)
        {
            Semestercourses semestercourses = db.SemesterCourses.Find(id);
            if (semestercourses == null)
            {
                return HttpNotFound();
            }
            return View(semestercourses);
        }

        //
        // GET: /Semestercourses/Create

        public ActionResult Create()
        {

            List<object> newList = new List<object>();

            foreach (var semester in db.Semesters.Include(s => s.Program))
                newList.Add(new
                {
                    SemesterId = semester.SemesterID,
                    Name = semester.Program.Name + " [" + semester.Name + "]"
                });

            ViewBag.SemesterID = new SelectList(newList, "SemesterID", "Name");
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name");
            return View();
        }

        //
        // POST: /Semestercourses/Create

        [HttpPost]
        public ActionResult Create(Semestercourses semestercourses)
        {
            if (ModelState.IsValid)
            {
                db.SemesterCourses.Add(semestercourses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Name", semestercourses.SemesterID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", semestercourses.CourseID);
            return View(semestercourses);
        }

        //
        // GET: /Semestercourses/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Semestercourses semestercourses = db.SemesterCourses.Find(id);
            if (semestercourses == null)
            {
                return HttpNotFound();
            }
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Name", semestercourses.SemesterID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", semestercourses.CourseID);
            return View(semestercourses);
        }

        //
        // POST: /Semestercourses/Edit/5

        [HttpPost]
        public ActionResult Edit(Semestercourses semestercourses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(semestercourses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Name", semestercourses.SemesterID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Name", semestercourses.CourseID);
            return View(semestercourses);
        }

        //
        // GET: /Semestercourses/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Semestercourses semestercourses = db.SemesterCourses.Find(id);
            if (semestercourses == null)
            {
                return HttpNotFound();
            }
            return View(semestercourses);
        }

        //
        // POST: /Semestercourses/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Semestercourses semestercourses = db.SemesterCourses.Find(id);
            db.SemesterCourses.Remove(semestercourses);
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