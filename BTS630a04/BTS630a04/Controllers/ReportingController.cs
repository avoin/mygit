using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTS630a04.Models;
using System.Data;
using System.Data.Entity;

namespace BTS630a04.Controllers
{
    [Authorize(Roles = "Chair")]
    public class ReportingController : Controller
    {
        //
        // GET: /Reporting/

        private StaffingContext db = new StaffingContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TermStatus()
        {
            var termstatus = db.Professors.Include(p => p.TermStatus);
            return View(termstatus.ToList());
        }

        public ActionResult WorkStatus()
        {
            var workstatus = db.Professors.Include(p => p.WorkStatus);
            return View(workstatus.ToList());
        }


        public ActionResult CredentialBased()
        {
            var credentials = db.Professorcredentials.Include(c => c.Credentials).Include(c => c.Professor);
            return View(credentials.ToList());
        }

        public ActionResult DesignationBased()
        {
            var designation = db.Professors.Include(c => c.Designation);
            return View(designation.ToList());
        }

        public ActionResult DesignationCredentialBased()
        {
            var desigcred = db.Professorcredentials.Include(c => c.Credentials).Include(c => c.Professor);
            return View(desigcred.ToList());
        }

        public ActionResult CourseReport()
        {
            var courseRep = db.Teachings.Include(t => t.Professor).Include(t => t.Course);
            return View(courseRep.ToList());
        }
    }
}
