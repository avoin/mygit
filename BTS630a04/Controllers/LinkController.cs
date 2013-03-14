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
    public class LinkController : Controller
    {
        private StaffingContext db = new StaffingContext();

        //
        // GET: /Link/

        public ActionResult Index()
        {
            var links = db.Links.Include(l => l.Professor).Include(l => l.User).Include(l => l.Role);
            return View(links.ToList());
        }

        //
        // GET: /Link/Details/5

        public ActionResult Details(int id = 0)
        {
            Link link = db.Links.Find(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        //
        // GET: /Link/Create

        public ActionResult Create()
        {
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username");
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "Name");
            return View();
        }

        //
        // POST: /Link/Create

        [HttpPost]
        public ActionResult Create(Link link)
        {
            if (ModelState.IsValid)
            {
                db.Links.Add(link);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName", link.ProfessorID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", link.UserID);
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "Name", link.RoleID);
            return View(link);
        }

        //
        // GET: /Link/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Link link = db.Links.Find(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName", link.ProfessorID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", link.UserID);
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "Name", link.RoleID);
            return View(link);
        }

        //
        // POST: /Link/Edit/5

        [HttpPost]
        public ActionResult Edit(Link link)
        {
            if (ModelState.IsValid)
            {
                db.Entry(link).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName", link.ProfessorID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Username", link.UserID);
            ViewBag.RoleID = new SelectList(db.Roles, "RoleID", "Name", link.RoleID);
            return View(link);
        }

        //
        // GET: /Link/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Link link = db.Links.Find(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        //
        // POST: /Link/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Link link = db.Links.Find(id);
            db.Links.Remove(link);
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