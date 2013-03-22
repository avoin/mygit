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
    public class ProfessorController : Controller
    {
        private StaffingContext db = new StaffingContext();

        //
        // GET: /Professor/

        public ActionResult Index()
        {
            var professors = db.Professors.Include(p => p.TermStatus).Include(p => p.WorkStatus).Include(p => p.Location).Include(p => p.Designation);
            return View(professors.ToList());
        }

        //
        // GET: /Professor/Details/5

        public ActionResult Details(int id = 0)
        {
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        //
        // GET: /Professor/Create

        public ActionResult Create()
        {
            ViewBag.TermStatusID = new SelectList(db.TermStatus, "TermStatusID", "Status");
            ViewBag.WorkStatusID = new SelectList(db.WorkStatus, "WorkStatusID", "Status");
            ViewBag.LocationID = new SelectList(db.Location, "LocationID", "Name");
            ViewBag.DesignationID = new SelectList(db.Designation, "DesignationID", "Title");
            return View();
        }

        //
        // POST: /Professor/Create

        [HttpPost]
        public ActionResult Create(Professor professor)
        {
            if (ModelState.IsValid)
            {
                db.Professors.Add(professor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TermStatusID = new SelectList(db.TermStatus, "TermStatusID", "Status", professor.TermStatusID);
            ViewBag.WorkStatusID = new SelectList(db.WorkStatus, "WorkStatusID", "Status", professor.WorkStatusID);
            ViewBag.LocationID = new SelectList(db.Location, "LocationID", "Name", professor.LocationID);
            ViewBag.DesignationID = new SelectList(db.Designation, "DesignationID", "Title", professor.DesignationID);
            return View(professor);
        }

        //
        // GET: /Professor/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            ViewBag.TermStatusID = new SelectList(db.TermStatus, "TermStatusID", "Status", professor.TermStatusID);
            ViewBag.WorkStatusID = new SelectList(db.WorkStatus, "WorkStatusID", "Status", professor.WorkStatusID);
            ViewBag.LocationID = new SelectList(db.Location, "LocationID", "Name", professor.LocationID);
            ViewBag.DesignationID = new SelectList(db.Designation, "DesignationID", "Title", professor.DesignationID);
            return View(professor);
        }

        //
        // POST: /Professor/Edit/5

        [HttpPost]
        public ActionResult Edit(Professor professor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(professor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TermStatusID = new SelectList(db.TermStatus, "TermStatusID", "Status", professor.TermStatusID);
            ViewBag.WorkStatusID = new SelectList(db.WorkStatus, "WorkStatusID", "Status", professor.WorkStatusID);
            ViewBag.LocationID = new SelectList(db.Location, "LocationID", "Name", professor.LocationID);
            ViewBag.DesignationID = new SelectList(db.Designation, "DesignationID", "Title", professor.DesignationID);
            return View(professor);
        }

        //
        // GET: /Professor/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Professor professor = db.Professors.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        //
        // POST: /Professor/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Professor professor = db.Professors.Find(id);
            db.Professors.Remove(professor);
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