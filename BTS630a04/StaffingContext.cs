using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BTS630a04.Models;
using System.Data.Entity;

namespace BTS630a04
{
    public class StaffingContext : DbContext
    {
        public DbSet<Program> Programs { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Credentials> Credentials { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WorkStatus> WorkStatus { get; set; }
        public DbSet<TermStatus> TermStatus { get; set; }
        public DbSet<Location> Location { get; set; }

        public DbSet<Teaching> Teachings { get; set; }

        public DbSet<Link> Links { get; set; }
        public DbSet<Designation> Designation { get; set; }
        public DbSet<CredentialsType> CredentialsType { get; set; }

        public DbSet<Semestercourses> SemesterCourses { get; set; }

        public DbSet<Programcourses> Programcourses { get; set; }
        

    }
}