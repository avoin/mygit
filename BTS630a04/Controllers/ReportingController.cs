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
            var professors = db.Professors.Include(p => p.TermStatus);
            return View(professors.ToList());
        }

        public ActionResult WorkStatus()
        {
            var professors = db.Professors.Include(p => p.WorkStatus);
            return View(professors.ToList());
        }

    }
}
