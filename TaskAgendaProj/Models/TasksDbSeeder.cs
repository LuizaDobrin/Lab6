using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAgendaProj.Models
{
    public class TasksDbSeeder
    {
        public static void Initialize(TasksDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any products.
            if (context.Tasks.Any())
            {
                return;   // DB has been seeded
            }

            context.Tasks.AddRange(
                //Date: mm/dd/yyyy
                new Task
                {
                    Title = "Contract DJ",
                    Description = "Semnare contract DJ - nunta",
                    DateTimeAdded = DateTime.Parse("05/03/2019 09:00"),
                    Deadline = DateTime.Parse("05/31/2019 09:00"),
                    Importance = "Low",
                    Status = "In_progress",
                    DateTimeClosedAt = DateTime.Parse(" 05/31/2019 09:00")
                },

                new Task
                {
                    Title = "Flori",
                    Description = "Contractare firma flori - pentru semnare contract nunta",
                    DateTimeAdded = DateTime.Parse("05/02/2019 12:00"),
                    Deadline = DateTime.Parse("06/10/2019 12:00"),
                    Importance = "Medium",
                    Status = "In_progress",
                    DateTimeClosedAt = DateTime.Parse("06/10/2019 12:00")
                },
                new Task
                {
                    Title = "Task3",
                    Description = "Description3",
                    DateTimeAdded = DateTime.Parse("05/10/2019 13:00"),
                    Deadline = DateTime.Parse("06/28/2019 13:00"),
                    Importance = "Low",
                    Status = "Open",
                    DateTimeClosedAt = DateTime.Parse("06/28/2019 13:00")
                },

                new Task
                {
                    Title = "Tema",
                    Description = "Tema lab 2 .Net",
                    DateTimeAdded = DateTime.Parse("05/04/2019 10:00"),
                    Deadline = DateTime.Parse("07/13/2019 11:00 "),
                    Importance = "High",
                    Status = "In_progress",
                    DateTimeClosedAt = DateTime.Parse("07/13/2019 11:00 ")
                }
            );

            context.SaveChanges();
        }

    }
}
