using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NIJ.Web.Models;

namespace NIJ.Web.Data
{
    public class IESDBInitializer
    {
        public static void Initialize(IESContext context)
        {
            context.Database.EnsureCreated();
            if (context.Projects.Any()&& context.Activities.Any())
            {
                return;
            }
            
            foreach(Project p in GetProjects())
            {
                context.Projects.Add(p);
            }            

            foreach (Activity a in GetActivies())
            {
                context.Activities.Add(a);
            }
            
            context.SaveChanges();
        }

        private static IEnumerable<Project> GetProjects()
        {
            return new Project[]
            {
                new Project { Name="Projeto 1"},
                new Project { Name="Projeto 2"}
            };
        }

        private static IEnumerable<Activity> GetActivies()
        {
            return new Activity[]
            {
                new Activity()
                {
                    ActivityId = 1,
                    Description = "Atividade 2",
                    StartedAt = DateTime.Now,
                    EndedAt = DateTime.Now.AddHours(1),
                    Status = Status.Started
                },
                new Activity()
                {
                    ActivityId = 2,
                    Description = "Atividade 3",
                    StartedAt = DateTime.Now,
                    EndedAt = DateTime.Now.AddHours(2),
                    Status = Status.Pause
                },
                new Activity()
                {
                    ActivityId = 3,
                    Description = "Atividade 3",
                    StartedAt = DateTime.Now,
                    EndedAt = DateTime.Now.AddHours(3),
                    Status = Status.Ended
                },
                new Activity()
                {
                    ActivityId = 4,
                    Description = "Atividade 4",
                    StartedAt = DateTime.Now,
                    EndedAt = DateTime.Now.AddHours(4),
                    Status = Status.Deleted
                }
            };

            
        }
    }
}
