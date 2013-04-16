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
    [Authorize(Roles = "Chair,Support")]
    public class SemesterController : Controller
    {
        private StaffingContext db = new StaffingContext();

        //
        // GET: /Semester/

        public ActionResult Index()
        {
            var semesters = db.Semesters.Include(s => s.Program);
            return View(semesters.ToList());
        }

        //
        // GET: /Semester/Details/5

        public ActionResult Details(int id = 0)
        {
            Semester semester = db.Semesters.Find(id);
            if (semester == null)
            {
                return HttpNotFound();
            }
            return View(semester);
        }

        //
        // GET: /Semester/Create

        public ActionResult Create()
        {
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name");
            return View();
        }

        //
        // POST: /Semester/Create

        [HttpPost]
        public ActionResult Create(Semester semester)
        {
            if (ModelState.IsValid)
            {
                db.Semesters.Add(semester);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name", semester.ProgramID);
            return View(semester);
        }

        //
        // GET: /Semester/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Semester semester = db.Semesters.Find(id);
            if (semester == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name", semester.ProgramID);
            return View(semester);
        }

        //
        // POST: /Semester/Edit/5

        [HttpPost]
        public ActionResult Edit(Semester semester)
        {
            if (ModelState.IsValid)
            {
                db.Entry(semester).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProgramID = new SelectList(db.Programs, "ProgramID", "Name", semester.ProgramID);
            return View(semester);
        }

        //
        // GET: /Semester/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Semester semester = db.Semesters.Find(id);
            if (semester == null)
            {
                return HttpNotFound();
            }
            return View(semester);
        }

        //
        // POST: /Semester/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Semester semester = db.Semesters.Find(id);
            db.Semesters.Remove(semester);
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