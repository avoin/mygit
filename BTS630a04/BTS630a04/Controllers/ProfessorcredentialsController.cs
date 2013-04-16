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
    public class ProfessorcredentialsController : Controller
    {
        private StaffingContext db = new StaffingContext();

        //
        // GET: /Professorcredentials/

        public ActionResult Index()
        {
            var professorcredentials = db.Professorcredentials.Include(p => p.Professor).Include(p => p.Credentials);
            return View(professorcredentials.ToList());
        }

        //
        // GET: /Professorcredentials/Details/5

        public ActionResult Details(int id = 0)
        {
            Professorcredentials professorcredentials = db.Professorcredentials.Find(id);
            if (professorcredentials == null)
            {
                return HttpNotFound();
            }
            return View(professorcredentials);
        }

        //
        // GET: /Professorcredentials/Create

        public ActionResult Create()
        {
            List<object> newList = new List<object>();

            foreach (var professor in db.Professors)
                newList.Add(new
                {
                    ProfessorId = professor.ProfessorID,
                    FirstName = professor.FirstName + " " + professor.LastName
                });

            ViewBag.ProfessorID = new SelectList(newList, "ProfessorID", "FirstName");
            ViewBag.CredentialsID = new SelectList(db.Credentials, "CredentialsID", "Name");
            return View();
        }

        //
        // POST: /Professorcredentials/Create

        [HttpPost]
        public ActionResult Create(Professorcredentials professorcredentials)
        {
            if (ModelState.IsValid)
            {
                db.Professorcredentials.Add(professorcredentials);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName", professorcredentials.ProfessorID);
            ViewBag.CredentialsID = new SelectList(db.Credentials, "CredentialsID", "Name", professorcredentials.CredentialsID);
            return View(professorcredentials);
        }

        //
        // GET: /Professorcredentials/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Professorcredentials professorcredentials = db.Professorcredentials.Find(id);
            if (professorcredentials == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName", professorcredentials.ProfessorID);
            ViewBag.CredentialsID = new SelectList(db.Credentials, "CredentialsID", "Name", professorcredentials.CredentialsID);
            return View(professorcredentials);
        }

        //
        // POST: /Professorcredentials/Edit/5

        [HttpPost]
        public ActionResult Edit(Professorcredentials professorcredentials)
        {
            if (ModelState.IsValid)
            {
                db.Entry(professorcredentials).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName", professorcredentials.ProfessorID);
            ViewBag.CredentialsID = new SelectList(db.Credentials, "CredentialsID", "Name", professorcredentials.CredentialsID);
            return View(professorcredentials);
        }

        //
        // GET: /Professorcredentials/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Professorcredentials professorcredentials = db.Professorcredentials.Find(id);
            if (professorcredentials == null)
            {
                return HttpNotFound();
            }
            return View(professorcredentials);
        }

        //
        // POST: /Professorcredentials/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Professorcredentials professorcredentials = db.Professorcredentials.Find(id);
            db.Professorcredentials.Remove(professorcredentials);
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