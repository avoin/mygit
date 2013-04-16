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
    public class CredentialsController : Controller
    {
        private StaffingContext db = new StaffingContext();

        //
        // GET: /Credentials/

        public ActionResult Index()
        {
            var credentials = db.Credentials.Include(c => c.Major).Include(c => c.CredentialsType);
            return View(credentials.ToList());
        }

        //
        // GET: /Credentials/Details/5

        public ActionResult Details(int id = 0)
        {
            Credentials credentials = db.Credentials.Find(id);
            if (credentials == null)
            {
                return HttpNotFound();
            }
            return View(credentials);
        }

        //
        // GET: /Credentials/Create

        public ActionResult Create()
        {
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "Name");
            ViewBag.CredentialsTypeID = new SelectList(db.CredentialsType, "CredentialsTypeID", "Type");
            return View();
        }

        //
        // POST: /Credentials/Create

        [HttpPost]
        public ActionResult Create(Credentials credentials)
        {
            if (ModelState.IsValid)
            {
                db.Credentials.Add(credentials);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "Name", credentials.MajorID);
            ViewBag.CredentialsTypeID = new SelectList(db.CredentialsType, "CredentialsTypeID", "Type", credentials.CredentialsTypeID);
            return View(credentials);
        }

        //
        // GET: /Credentials/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Credentials credentials = db.Credentials.Find(id);
            if (credentials == null)
            {
                return HttpNotFound();
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "Name", credentials.MajorID);
            ViewBag.CredentialsTypeID = new SelectList(db.CredentialsType, "CredentialsTypeID", "Type", credentials.CredentialsTypeID);
            return View(credentials);
        }

        //
        // POST: /Credentials/Edit/5

        [HttpPost]
        public ActionResult Edit(Credentials credentials)
        {
            if (ModelState.IsValid)
            {
                db.Entry(credentials).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "Name", credentials.MajorID);
            ViewBag.CredentialsTypeID = new SelectList(db.CredentialsType, "CredentialsTypeID", "Type", credentials.CredentialsTypeID);
            return View(credentials);
        }

        //
        // GET: /Credentials/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Credentials credentials = db.Credentials.Find(id);
            if (credentials == null)
            {
                return HttpNotFound();
            }
            return View(credentials);
        }

        //
        // POST: /Credentials/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Credentials credentials = db.Credentials.Find(id);
            db.Credentials.Remove(credentials);
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