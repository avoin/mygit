using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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
                new TermStatus {Status = "Full Time"},
                new TermStatus {Status = "Parial Load"},
                new TermStatus {Status = "Seasonal"}


            };

            termStatus.ForEach(t => context.TermStatus.Add(t));



            var locations = new List<Location> 
            { 
                new Location { Name = "Seneca@York"}
            
            };

            locations.ForEach( l => context.Location.Add(l));


            var roles = new List<Role> 
            {
                new Role { Name = "Chair" },
                new Role {Name = "Support Staff"},
                new Role { Name = "Professor"}
            };

            roles.ForEach(r => context.Roles.Add(r));

            context.SaveChanges();
        
        }
    }
}