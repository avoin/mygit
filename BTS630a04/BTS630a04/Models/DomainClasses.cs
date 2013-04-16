using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace BTS630a04.Models
{

    public class Program
    {
        public int ProgramID { get; set; }

        [DisplayName("Program")]
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
        
        public int SemesterID { get; set; }
        [DisplayName("Program")]
        public int ProgramID { get; set; }

        [DisplayName("Semester")]
        [Required(ErrorMessage = "A Name is required")]
        public String Name { get; set; }

        [Required(ErrorMessage = "The Year is required")]
        [Range(1990,3000)]
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

        [Required(ErrorMessage = "A Course Title is required")]
        [DisplayName("Course Title")]
        public String Name { get; set; }

        [Required(ErrorMessage = "A Description is required")]
        public String Description { get; set; }
        
        [MinLength(6, ErrorMessage = "You must enter 6 characters (do not include the section")]
        public String Code { get; set; }

        //Navigation Properties
        [DisplayName("Professor")]
        public virtual Professor Professor { get; set; }
 

    }

    public class Teaching {
        public int TeachingID { get; set; }
        public int ProfessorID { get; set; }
        public int CourseID { get; set; }
        public String Section { get; set; }

        //Navigation Properties
        public virtual Professor Professor { get; set; }
        public virtual Course Course { get; set; }
    }

    public class Major
    {
        
        public int MajorID { get; set; }

        [DisplayName("Major")]
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
        
        public int CredentialsID { get; set; }

        [DisplayName("Major")]
        public int MajorID { get; set; }

        [Required(ErrorMessage = "A Type is required")]
        [DisplayName("Type")]
        public int CredentialsTypeID { get; set; }

        [DisplayName("Credential")]
        [Required(ErrorMessage = "A Name is required")]
        public String Name { get; set; }

        [Required(ErrorMessage = "A Description is required")]
        public String Description { get; set; }

        [Required(ErrorMessage = "A Code is required")]
        public String Code { get; set; }

        //Navigation Properties
        public virtual Major Major { get; set; }
        [DisplayName("Professors")]
        public virtual List<Professor> ProfessorList { get; set; }
        public virtual CredentialsType CredentialsType { get; set; }

    }


    public class CredentialsType {
         [DisplayName("Type")]
        public int CredentialsTypeID { get; set; }
        public String Type { get; set; }

        //Navigation Properties
        public virtual List<Credentials> CredentialsList { get; set; }
    }

    public class WorkStatus {

       
        public int WorkStatusID { get; set; }
         [DisplayName("Work Status")]
        public String Status { get; set; }

        //Navigation Properties
        [DisplayName("Professors")]
        public virtual List<Professor> ProfessorList { get; set; }

    }

    public class TermStatus
    {
       
        public int TermStatusID { get; set; }
        [DisplayName("Term Status")]
        public String Status { get; set; }

        //Navigation Properties
        public virtual List<Professor> ProfessorList { get; set; }

    }

    public class Designation
    {

        public int DesignationID { get; set; }
        [DisplayName("Designation")]
        public String Title { get; set; }

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

        [DisplayName("Designation")]
        public int DesignationID { get; set; }

        //Navigation Properties
        [DisplayName("Term Status")]
        public virtual TermStatus TermStatus { get; set; }
        [DisplayName("Work Status")]
        public virtual WorkStatus WorkStatus { get; set; }
        [DisplayName("Experience")]
        public virtual List<Experience> ExperienceList { get; set; }
        [DisplayName("Credentials")]
        public virtual List<Credentials> CredentialsList { get; set; }

        [DisplayName("Courses")]
        public virtual List<Course> CourseList { get; set; }

        [DisplayName("Designation")]
        public virtual Designation Designation { get; set; }
        


    }

    public class Professorlocation {
        public int ProfessorlocationID { get; set; }
        public int LocationID { get; set; }
        public int ProfessorID { get; set; }


        //Navigation Properties
        public virtual Location Location { get; set; }
        public virtual Professor Professor { get; set; }
    }

    public class Location
    {
        
        public int LocationID { get; set; }
        [DisplayName("Location")]
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

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Start Date")]
        
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "An End Date is required")]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("End Date")]
        
        public DateTime EndDate { get; set; }

        //Navigation Properties
        [DisplayName("Professor")]
        public virtual Professor Professor { get; set; }
    }


    public class Link {
        public int LinkID { get; set; }
        public Nullable<int> ProfessorID { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "You must assign a role to the user")]
        public String Role { get; set; }

        //Navigation Properties
        public virtual Professor Professor { get; set; }

    }

    public class Semestercourses {
        public int SemestercoursesID { get; set; }
        //public int ProgramID { get; set; }
        public int SemesterID { get; set; }
        public int CourseID { get; set; }

        //Navigation Properties
        public virtual Semester Semester { get; set; }
        public virtual Course Course { get; set; }
    
    }

    public class Programcourses
    {
        public int ProgramcoursesID { get; set; }
        public int ProgramID { get; set; }
        //public int SemesterID { get; set; }
        public int CourseID { get; set; }

        //Navigation Properties
        public virtual Program Program { get; set; }
        public virtual Course Course { get; set; }

    }

    public class Professorcredentials
    {
        public int ProfessorCredentialsID { get; set; }
        public int ProfessorID { get; set; }
        public int CredentialsID { get; set; }

        //Navigation Properties
        public virtual Professor Professor { get; set; }
        public virtual Credentials Credentials { get; set; }

    }

}