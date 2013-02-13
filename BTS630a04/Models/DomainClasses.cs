using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BTS630a04.Models
{

    public class Program
    {
        public int ProgramID { get; set; }
        
        [Required(ErrorMessage = "An Name is required")]
        public String Name { get; set; }

        [Required(ErrorMessage = "A Description is required")]
        public String Description { get; set; }

        [Required(ErrorMessage = "A Program Code is required")]
        public String Code { get; set; }

        //Navigation Properties
        [DisplayName("Semesters")]
        public virtual List<Semester> SemesterList { get; set; }
    }

    public class Semester
    {
        [DisplayName("Semester")]
        public int SemesterID { get; set; }
        [DisplayName("Program")]
        public int ProgramID { get; set; }
       
        [Required(ErrorMessage = "A Name is required")]
        public String Name { get; set; }

        [Required(ErrorMessage = "The Year is required")]
        public int Year { get; set; }

        //Navigation Properties
        public virtual Program Program { get; set; }
        [DisplayName("Course List")]
        public virtual List<Course> CourseList { get; set; }

    }

    public class Course
    {
        [DisplayName("Course")]
        public int CourseID { get; set; }
        [DisplayName("Semester")]
        public int SemesterID { get; set; }

        [Required(ErrorMessage = "A Name is required")]
        public String Name { get; set; }

        [Required(ErrorMessage = "A Description is required")]
        public String Description { get; set; }
        
        [MinLength(7, ErrorMessage = "You must enter 7-8 characters which includes the section i.e. BTP600A, BTP600AB")]
        [MaxLength(8, ErrorMessage = "You must enter 7-8 characters which includes the section i.e. BTP600A, BTP600AB")]
        public String Code { get; set; }

        //Navigation Properties
        [DisplayName("Semester")]
        public virtual Semester Semester { get; set; }

    }

    public class Major
    {
        [DisplayName("Major")]
        public int MajorID { get; set; }

        [Required(ErrorMessage = "A Name is required")]
        public String Name { get; set; }

        [Required(ErrorMessage = "A Description is required")]
        public String Description { get; set; }

        //Navigation Properties
        [DisplayName("Credentials")]
        public virtual List<Credentials> CredentialsList { get; set; }


    }

    public class Credentials
    {
        [DisplayName("Credentials")]
        public int CredentialsID { get; set; }

        [DisplayName("Major")]
        public int MajorID { get; set; }

        [Required(ErrorMessage = "A Name is required")]
        public String Name { get; set; }

        [Required(ErrorMessage = "A Description is required")]
        public String Description { get; set; }

        [Required(ErrorMessage = "A Code is required")]
        public String Code { get; set; }

        [Required(ErrorMessage = "A Type is required")]
        public String Type { get; set; }

        //Navigation Properties
        public virtual Major Major { get; set; }
        [DisplayName("Professors")]
        public virtual List<Professor> ProfessorList { get; set; }

    }

    public class WorkStatus {

        [DisplayName("Work Status")]
        public int WorkStatusID { get; set; }
        public String Status { get; set; }

        //Navigation Properties
        [DisplayName("Professors")]
        public virtual List<Professor> ProfessorList { get; set; }

    }

    public class TermStatus
    {
        [DisplayName("Term Status")]
        public int TermStatusID { get; set; }
        public String Status { get; set; }

        //Navigation Properties
        public virtual List<Professor> ProfessorList { get; set; }

    }

    public class Professor
    {
        [DisplayName("Professor")]
        public int ProfessorID { get; set; }


        [Required(ErrorMessage = "A First Name is required")]
        [DisplayName("First Name")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "A Last Name is required")]
        [DisplayName("Last Name")]
        public String LastName { get; set; }

        [DisplayName("Work Status")]
        public int WorkStatusID { get; set; }

        [DisplayName("Term Status")]
        public int TermStatusID { get; set; }

        [DisplayName("Location")]
        public int LocationID { get; set; }

        //Navigation Properties
        [DisplayName("Term Status")]
        public virtual TermStatus TermStatus { get; set; }
        [DisplayName("Work Status")]
        public virtual WorkStatus WorkStatus { get; set; }
        [DisplayName("Experience")]
        public virtual List<Experience> ExperienceList { get; set; }
        [DisplayName("Credentials")]
        public virtual List<Credentials> CredentialsList { get; set; }

        [DisplayName("Location")]
        public virtual Location Location { get; set; }

    }

    public class Location
    {
        [DisplayName("Location")]
        public int LocationID { get; set; }
        public String Name { get; set; }

        //Navigation Properties
        [DisplayName("Professor")]
        public virtual List<Professor> ProfessorList { get; set; }
    }

    public class Experience
    {
        [DisplayName("Experience")]
        public int ExperienceID { get; set; }

        [DisplayName("Professor")]
        public int ProfessorID { get; set; }

        [Required(ErrorMessage = "A Description is required")]
        public String Description { get; set; }

        [Required(ErrorMessage = "A Start Date is required")]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "An End Date is required")]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        //Navigation Properties
        [DisplayName("Professor")]
        public virtual List<Professor> ProfessorList { get; set; }
    }

    public class Role
    {
        [DisplayName("Role")]
        public int RoleID { get; set; }

        [Required(ErrorMessage = "An Name is required")]
        public String Name { get; set; }

        //Navigation Properties
        public virtual List<User> UserList { get; set; }
    }

    public class User
    {
        [DisplayName("User")]
        public int UserID { get; set; }
        [DisplayName("Role")]
        public int RoleID { get; set; }
        [DisplayName("Professor")]
        public int? ProfessorID { get; set; }

        [Required(ErrorMessage = "An Name is required")]
        public String Name { get; set; }

        [Required(ErrorMessage = "A Password is required")]
        public String Password { get; set; }

        

        //Navigation Properties
        public virtual Professor Professor { get; set; }
        public virtual Role Role { get; set; }
    }



}