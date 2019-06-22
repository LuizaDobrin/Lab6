using TaskAgendaProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;



namespace TaskAgendaProj.ViewModels
{
    public class TaskPostModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public DateTime Deadline { get; set; }
        public string Importance { get; set; } 
        public string Status { get; set; }
        public DateTime? DateTimeClosedAt { get; set; }

        public List<Comment> Comments { get; set; }

        public static Task ToTask(TaskPostModel task)
        {
            if (task.Importance == "Medium")
            {
            }
            else if (task.Importance == "High")
            {
            }
            if (task.Status == "In_Progress")
            {
            }
            else if (task.Status == "Closed")
            {
            }

            return new Task
            {
                Title = task.Title,
                Description = task.Description,
                DateTimeAdded = task.DateTimeAdded,
                Deadline = task.Deadline,
                Importance = task.Importance,
                Status = task.Status,
                DateTimeClosedAt = task.DateTimeClosedAt,
                Comments = task.Comments
            };
        }

    }
}
