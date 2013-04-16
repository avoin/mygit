using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Security;

namespace BTS630a04.Models
{
    //WARNING -------  CHANGE DROP CREATE LINE WHEN IN PRODUCTION
    public class DBInitialize : DropCreateDatabaseIfModelChanges<StaffingContext>
    {
        protected override void Seed(StaffingContext context){
            var workStatus = new List<WorkStatus>
            {
                new WorkStatus{ Status = "Active"},
                new WorkStatus {Status = "Inactive"}

            };

            workStatus.ForEach(w => context.WorkStatus.Add(w));

            var termStatus = new List<TermStatus>
            {
                new TermStatus{ Status = "Part-Time"},
                new TermStatus {Status = "Full-Time"},
                new TermStatus {Status = "Parial-Load"},
                new TermStatus {Status = "Sessional"}


            };

            termStatus.ForEach(t => context.TermStatus.Add(t));



            var locations = new List<Location> 
            { 
                new Location { Name = "King"},
                new Location { Name = "Seneca@York"},
                new Location { Name = "Markham"}
            
            };

            locations.ForEach( l => context.Location.Add(l));

            var designation = new List<Designation>
            {
                new Designation{ Title = "Professor"},
                new Designation {Title = "Chair"},
                new Designation { Title = "Co-Ordinator"}
             
            };

            designation.ForEach(t => context.Designation.Add(t));

            var credentialsType = new List<CredentialsType>
            {
                new CredentialsType { Type = "Degree" },
                new CredentialsType { Type = "Diploma" },
                new CredentialsType { Type = "Certification" }
            };

            credentialsType.ForEach(c => context.CredentialsType.Add(c));

            //Install membership services
            SqlServices.Install(context.Database.Connection.Database, SqlFeatures.All, context.Database.Connection.ConnectionString);

            //Add roles to the system (Membership)
            if (Roles.GetAllRoles().Length == 0)
            {
                Roles.CreateRole("User");
                Roles.CreateRole("Chair");
                Roles.CreateRole("Professor");
                Roles.CreateRole("Support");
            }


            //TEST DATA
            
            var major = new List<Major>
            {
                new Major{ Name = "Math", Description = "Mathematics" },
                new Major{ Name = "English", Description = "The study of language" }
                
            };

            major.ForEach(m => context.Majors.Add(m));
            
            var credentials = new List<Credentials>
            {
                new Credentials{ Name = "Math Diploma", Description = "Mathematics Diploma", CredentialsType = credentialsType.ElementAt(1), Major = major.ElementAt(0), Code = "MAD" },
                new Credentials{ Name = "Math Degree", Description = "Mathematics Degree", CredentialsType = credentialsType.ElementAt(0), Major = major.ElementAt(0), Code = "MADEG" }
                
            };

            credentials.ForEach(m => context.Credentials.Add(m));
            
            var professor = new List<Professor>
            {
                new Professor{ FirstName = "Nick", LastName = "Mirabella", Designation = designation.ElementAt(1), WorkStatus = workStatus.ElementAt(1), TermStatus = termStatus.ElementAt(0) },
                //new Professor{ FirstName = "Barath", LastName = "Kumar", Designation = designation.ElementAt(0), WorkStatus = workStatus.ElementAt(0), TermStatus = termStatus.ElementAt(1) }
                
                
            };

            professor.ForEach(p => context.Professors.Add(p));
            
            var experience = new List<Experience>
            {
                new Experience{ Description = "CIBC", StartDate = new DateTime(1990,02,01), EndDate = new DateTime(2011,03,17) }
               
                
            };

            experience.ForEach(m => context.Experiences.Add(m));
            
            var program = new List<Program>
            {
                new Program{ Name = "Software Development", Description = "B. of Software Development", Code = "BSD" },
                new Program{ Name = "Information and Security", Description = "B. of Information and Security", Code = "IFS" }
               
                
            };

            program.ForEach(m => context.Programs.Add(m));
            
            var semester = new List<Semester>
            {
                new Semester{ Name = "Fall", Program = program.ElementAt(0), Year = 2011 },
                new Semester{ Name = "Winter", Program = program.ElementAt(1), Year = 2011 }
               
                
            };

            semester.ForEach(m => context.Semesters.Add(m));
            
            var course = new List<Course>
            {
                new Course{ Name = "Java Programming", Code = "BTP400", Description = "Class for Java programming" },
                new Course{ Name = "C Programming", Code = "BTP100", Description = "Class for C programming" }
                
                
            };

            course.ForEach(m => context.Courses.Add(m));


            var semestercourses = new List<Semestercourses>
            {
                new Semestercourses{ Semester = semester.ElementAt(0), Course = course.ElementAt(0) },
                new Semestercourses{ Semester = semester.ElementAt(1), Course = course.ElementAt(1) }
                
                
            };

            semestercourses.ForEach(m => context.SemesterCourses.Add(m));

            var teaching= new List<Teaching>
            {
                new Teaching{ Course = course.ElementAt(0), Professor = professor.ElementAt(0), Section = "A" },
                //new Teaching{ Course = course.ElementAt(1), Professor = professor.ElementAt(1), Section = "B" }
               
                
            };

            teaching.ForEach(m => context.Teachings.Add(m));


            
            
            var professorcredentials = new List<Professorcredentials>
            {
                new Professorcredentials{ Professor = professor.ElementAt(0), Credentials = credentials.ElementAt(1)},
                //new Professorcredentials{ Professor = professor.ElementAt(1), Credentials = credentials.ElementAt(0)}
                
            };

            professorcredentials.ForEach(m => context.Professorcredentials.Add(m));


            //END TEST DATA

            context.SaveChanges();
        
        }
    }
}