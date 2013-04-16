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
    public class ProfileController : Controller
    {
        string professorquery = "SELECT     Professors.FirstName, Professors.LastName, TermStatus.Status AS TermStatus, Designations.Title, WorkStatus.Status AS WorkStatus, WorkStatus.WorkStatusID,                      Designations.DesignationID, TermStatus.TermStatusID, Professors.ProfessorID FROM         Professors INNER JOIN                      Links ON Professors.ProfessorID = Links.ProfessorID INNER JOIN                      WorkStatus ON Professors.WorkStatusID = WorkStatus.WorkStatusID INNER JOIN                      TermStatus ON Professors.TermStatusID = TermStatus.TermStatusID INNER JOIN                      Designations ON Professors.DesignationID = Designations.DesignationID";
        string termstatusquery ="SELECT     TermStatus.Status, TermStatus.TermStatusID FROM         Professors INNER JOIN                      Links ON Professors.ProfessorID = Links.ProfessorID INNER JOIN                      WorkStatus ON Professors.WorkStatusID = WorkStatus.WorkStatusID INNER JOIN                      TermStatus ON Professors.TermStatusID = TermStatus.TermStatusID INNER JOIN                      Designations ON Professors.DesignationID = Designations.DesignationID";
        string workstatusquery = "SELECT     WorkStatus.Status, WorkStatus.WorkStatusID FROM         Professors INNER JOIN                      Links ON Professors.ProfessorID = Links.ProfessorID INNER JOIN                     WorkStatus ON Professors.WorkStatusID = WorkStatus.WorkStatusID INNER JOIN                      TermStatus ON Professors.TermStatusID = TermStatus.TermStatusID INNER JOIN                      Designations ON Professors.DesignationID = Designations.DesignationID";
        string designationquery = "SELECT     Designations.DesignationID, Designations.Title FROM         Professors INNER JOIN                      Links ON Professors.ProfessorID = Links.ProfessorID INNER JOIN                      WorkStatus ON Professors.WorkStatusID = WorkStatus.WorkStatusID INNER JOIN                      TermStatus ON Professors.TermStatusID = TermStatus.TermStatusID INNER JOIN                      Designations ON Professors.DesignationID = Designations.DesignationID";
        string teachingquery = "SELECT     Links.UserName, Teachings.TeachingID, Teachings.Section, Teachings.ProfessorID, Teachings.CourseID FROM         Links INNER JOIN                      Professors ON Links.ProfessorID = Professors.ProfessorID INNER JOIN                      Teachings ON Professors.ProfessorID = Teachings.ProfessorID INNER JOIN                      Courses ON Teachings.CourseID = Courses.CourseID ";
        string coursequery = "SELECT     Links.UserName, Courses.CourseID, Courses.Name, Courses.Description, Courses.Code FROM         Links INNER JOIN                      Professors ON Links.ProfessorID = Professors.ProfessorID INNER JOIN                      Teachings ON Professors.ProfessorID = Teachings.ProfessorID INNER JOIN                      Courses ON Teachings.CourseID = Courses.CourseID";
        
        //
        // GET: /Profile

        private StaffingContext db = new StaffingContext();

        public class ProfileObject 
        {
            
            public string UserName { get; set; }
            public string Role { get; set; }

            public Professor professor { get; set; }
            public TermStatus termstatus { get; set; }
            public WorkStatus workstatus { get; set; }
            public Designation designation { get; set; }

            public List<Teaching> teachings { get; set; }
            public List<Course> courses { get; set; }

        }
             

        public ActionResult Index()
        {
            String username = Membership.GetUser().UserName;


            return RedirectToAction("UserName", username);

        }
        public ActionResult UserName(string id)
        {
            
            var p = db.Database.SqlQuery<Professor>(professorquery + " where (Links.UserName = '"+id+"') ");
            var t = db.Database.SqlQuery<TermStatus>(termstatusquery + " where (Links.UserName = '" + id + "') ");
            var w = db.Database.SqlQuery<WorkStatus>(workstatusquery + " where (Links.UserName = '" + id + "') ");
            var d = db.Database.SqlQuery<Designation>(designationquery + " where (Links.UserName = '" + id + "') ");
            var teach = db.Database.SqlQuery<Teaching>(teachingquery + " where (Links.UserName = '" + id + "') ");
            var course = db.Database.SqlQuery<Course>(coursequery + " where (Links.UserName = '" + id + "') ");

            ProfileObject profile = new ProfileObject();
            profile.professor = p != null && p.Any() ? p.First<Professor>() : null;
            profile.termstatus = t != null && t.Any() ? t.First<TermStatus>() : null;
            profile.workstatus = w != null && w.Any() ? w.First<WorkStatus>() : null;
            profile.designation = d != null && d.Any() ? d.First<Designation>() : null;
            profile.teachings = teach.ToList();
            profile.courses = course.ToList();
            profile.UserName = id;
            return View(profile);

        }

        public ActionResult MemberList()
        {

            var professor = db.Links.Where(i => i.Role == "Professor");
            return View(professor.ToList());

        }
    }
}
