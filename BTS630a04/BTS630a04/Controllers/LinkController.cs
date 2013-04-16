using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BTS630a04.Models;

namespace BTS630a04.Controllers
{
    [Authorize(Roles = "Chair")]
    public class LinkController : Controller
    {
        private StaffingContext db = new StaffingContext();

        //
        // GET: /Link/

        public ActionResult Index()
        {

            var links = db.Links.Include(l => l.Professor);
            return View(links.ToList());
        }


        //
        // GET: /Link/Create

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
            return View();
        }

        //
        // POST: /Link/Create

        [HttpPost]
        public ActionResult Create(Link link)
        {
            if (ModelState.IsValid)
            {

                //Do not allow user to be assigned to a role he is already in
                if (!Roles.IsUserInRole(link.UserName, link.Role))
                {
                    db.Links.Add(link);
                    db.SaveChanges();

                    //Adds role to a user (Membership)
                    Roles.AddUserToRole(link.UserName, link.Role);
                }

                return RedirectToAction("Index");
            }

            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName", link.ProfessorID);
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
            if (Roles.IsUserInRole(link.UserName, link.Role))
            {
                db.Links.Remove(link);
                db.SaveChanges();

                //Remove user from role (Membership)

                Roles.RemoveUserFromRole(link.UserName, link.Role);
            }
                return RedirectToAction("Index");
            
        }

        public JsonResult AutocompleteUsername(string term)
        {
            if (term != null)
            {
                List<String> suggestions = new List<String>();

                MembershipUserCollection muc = Membership.GetAllUsers();

                foreach (MembershipUser user in muc)
                {
                    suggestions.Add(user.UserName);
                }

                var namelist = suggestions.Where(n => n.ToLower().StartsWith(term.ToLower()));

                return Json(namelist, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AutocompleteRole(string term)
        {

            String[] suggestions = System.Web.Security.Roles.GetAllRoles();

            var namelist = suggestions.Where(n => n.ToLower().StartsWith(term.ToLower()));

            return Json(namelist, JsonRequestBehavior.AllowGet);





        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}