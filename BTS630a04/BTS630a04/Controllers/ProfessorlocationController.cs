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
    public class ProfessorlocationController : Controller
    {
        private StaffingContext db = new StaffingContext();

        //
        // GET: /Professorlocation/

        public ActionResult Index()
        {
            var professorlocations = db.Professorlocations.Include(p => p.Location).Include(p => p.Professor);
            return View(professorlocations.ToList());
        }

        //
        // GET: /Professorlocation/Details/5

        public ActionResult Details(int id = 0)
        {
            Professorlocation professorlocation = db.Professorlocations.Find(id);
            if (professorlocation == null)
            {
                return HttpNotFound();
            }
            return View(professorlocation);
        }

        //
        // GET: /Professorlocation/Create

        public ActionResult Create()
        {
            List<object> newList = new List<object>();

            foreach (var professor in db.Professors)
                newList.Add(new
                {
                    ProfessorId = professor.ProfessorID,
                    FirstName = professor.FirstName + " " + professor.LastName
                });

            ViewBag.LocationID = new SelectList(db.Location, "LocationID", "Name");
            ViewBag.ProfessorID = new SelectList(newList, "ProfessorID", "FirstName");
            return View();
        }

        //
        // POST: /Professorlocation/Create

        [HttpPost]
        public ActionResult Create(Professorlocation professorlocation)
        {
            if (ModelState.IsValid)
            {
                db.Professorlocations.Add(professorlocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Location, "LocationID", "Name", professorlocation.LocationID);
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName", professorlocation.ProfessorID);
            return View(professorlocation);
        }

        //
        // GET: /Professorlocation/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Professorlocation professorlocation = db.Professorlocations.Find(id);
            if (professorlocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.Location, "LocationID", "Name", professorlocation.LocationID);
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName", professorlocation.ProfessorID);
            return View(professorlocation);
        }

        //
        // POST: /Professorlocation/Edit/5

        [HttpPost]
        public ActionResult Edit(Professorlocation professorlocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(professorlocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Location, "LocationID", "Name", professorlocation.LocationID);
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName", professorlocation.ProfessorID);
            return View(professorlocation);
        }

        //
        // GET: /Professorlocation/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Professorlocation professorlocation = db.Professorlocations.Find(id);
            if (professorlocation == null)
            {
                return HttpNotFound();
            }
            return View(professorlocation);
        }

        //
        // POST: /Professorlocation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Professorlocation professorlocation = db.Professorlocations.Find(id);
            db.Professorlocations.Remove(professorlocation);
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