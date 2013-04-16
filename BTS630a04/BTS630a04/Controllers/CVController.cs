using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTS630a04.Models;
using System.Data;
using System.Data.Entity;
using System.Web.Security;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTS630a04.Controllers
{

    [Authorize(Roles = "User")]
    public class CVController : Controller
    {

        //designation, experience, location, credentials, professor
        string professorquery = "SELECT     Professors.FirstName, Professors.LastName, TermStatus.Status AS TermStatus, Designations.Title, WorkStatus.Status AS WorkStatus, WorkStatus.WorkStatusID,                      Designations.DesignationID, TermStatus.TermStatusID, Professors.ProfessorID FROM         Professors INNER JOIN                      Links ON Professors.ProfessorID = Links.ProfessorID INNER JOIN                      WorkStatus ON Professors.WorkStatusID = WorkStatus.WorkStatusID INNER JOIN                      TermStatus ON Professors.TermStatusID = TermStatus.TermStatusID INNER JOIN                      Designations ON Professors.DesignationID = Designations.DesignationID";
        string termstatusquery = "SELECT     TermStatus.Status, TermStatus.TermStatusID FROM         Professors INNER JOIN                      Links ON Professors.ProfessorID = Links.ProfessorID INNER JOIN                      WorkStatus ON Professors.WorkStatusID = WorkStatus.WorkStatusID INNER JOIN                      TermStatus ON Professors.TermStatusID = TermStatus.TermStatusID INNER JOIN                      Designations ON Professors.DesignationID = Designations.DesignationID";
        string workstatusquery = "SELECT     WorkStatus.Status, WorkStatus.WorkStatusID FROM         Professors INNER JOIN                      Links ON Professors.ProfessorID = Links.ProfessorID INNER JOIN                     WorkStatus ON Professors.WorkStatusID = WorkStatus.WorkStatusID INNER JOIN                      TermStatus ON Professors.TermStatusID = TermStatus.TermStatusID INNER JOIN                      Designations ON Professors.DesignationID = Designations.DesignationID";
        string designationquery = "SELECT     Designations.DesignationID, Designations.Title FROM         Professors INNER JOIN                      Links ON Professors.ProfessorID = Links.ProfessorID INNER JOIN                      WorkStatus ON Professors.WorkStatusID = WorkStatus.WorkStatusID INNER JOIN                      TermStatus ON Professors.TermStatusID = TermStatus.TermStatusID INNER JOIN                      Designations ON Professors.DesignationID = Designations.DesignationID";
        string credentialquery = "SELECT     Credentials.CredentialsID, Credentials.MajorID, Credentials.CredentialsTypeID, Credentials.Name, Credentials.Description, Credentials.Code FROM         Links INNER JOIN                      Professors ON Links.ProfessorID = Professors.ProfessorID INNER JOIN                      Professorcredentials ON Professors.ProfessorID = Professorcredentials.ProfessorID INNER JOIN                      Credentials ON Professorcredentials.CredentialsID = Credentials.CredentialsID";
        string majorquery = "SELECT     Majors.* FROM         Links INNER JOIN                       Professors ON Links.ProfessorID = Professors.ProfessorID INNER JOIN                      Professorcredentials ON Professors.ProfessorID = Professorcredentials.ProfessorID INNER JOIN                      Credentials ON Professorcredentials.CredentialsID = Credentials.CredentialsID INNER JOIN                      Majors ON Credentials.MajorID = Majors.MajorID";
        string locationquery = "SELECT     Locations.* FROM         Links INNER JOIN                      Professors ON Links.ProfessorID = Professors.ProfessorID INNER JOIN                      Professorlocations ON Professors.ProfessorID = Professorlocations.ProfessorID INNER JOIN                      Locations ON Professorlocations.LocationID = Locations.LocationID";
        string experiencequery = "SELECT     Experiences.* FROM         Links INNER JOIN                      Professors ON Links.ProfessorID = Professors.ProfessorID INNER JOIN                      Experiences ON Professors.ProfessorID = Experiences.ProfessorID";
           

        private StaffingContext db = new StaffingContext();

        public class CVObject
        {

            public string UserName { get; set; }
            public string Role { get; set; }

            public Professor professor { get; set; }
            public TermStatus termstatus { get; set; }
            public WorkStatus workstatus { get; set; }
            public Designation designation { get; set; }

            public List<Credentials> credentials { get; set; }
            public List<Major> majors { get; set; }
            public List<Location> locations { get; set; }
            public List<Experience> experiences { get; set; }

        }


        public ActionResult Index()
        {
            String username = Membership.GetUser().UserName;


            return RedirectToAction("UserName", username);

        }
        public ActionResult UserName(string id)
        {

            var p = db.Database.SqlQuery<Professor>(professorquery + " where (Links.UserName = '" + id + "') ");
            var t = db.Database.SqlQuery<TermStatus>(termstatusquery + " where (Links.UserName = '" + id + "') ");
            var w = db.Database.SqlQuery<WorkStatus>(workstatusquery + " where (Links.UserName = '" + id + "') ");
            var d = db.Database.SqlQuery<Designation>(designationquery + " where (Links.UserName = '" + id + "') ");
            var credential = db.Database.SqlQuery<Credentials>(credentialquery + " where (Links.UserName = '" + id + "') ");
            var major = db.Database.SqlQuery<Major>(majorquery + " where (Links.UserName = '" + id + "') ");
            var location = db.Database.SqlQuery<Location>(locationquery + " where (Links.UserName = '" + id + "') ");
            var experience = db.Database.SqlQuery<Experience>(experiencequery + " where (Links.UserName = '" + id + "') ");

            CVObject cv = new CVObject();
            cv.professor = p != null && p.Any() ? p.First<Professor>() : null ;
            cv.termstatus = t != null && t.Any() ? t.First<TermStatus>() : null ;
            cv.workstatus = w != null && w.Any() ? w.First<WorkStatus>() : null ;
            cv.designation = d != null && d.Any() ? d.First<Designation>() : null ;
            cv.credentials = credential.ToList();
            cv.majors = major.ToList();
            cv.locations = location.ToList();
            cv.experiences = experience.ToList();
            cv.UserName = id;
            return View(cv);

        }

        public ActionResult MemberList()
        {

            var professor = db.Links.Where(i => i.Role == "Professor");      
            return View(professor.ToList());

        }

    }
}
