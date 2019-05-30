using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAgendaProj.Models
{
    public enum Importance
    {
        Low,
        Medium,
        High
    }
    public enum Status
    {
        Open,
        In_progress,
        Closed
    }
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public DateTime Deadline { get; set; }
        // importance: low, medium, high
        [EnumDataType(typeof(Importance))]
        public string Importance { get; set; }
        // status: open, in progress, closed
        [EnumDataType(typeof(Status))]
        public string Status { get; set; }
        public DateTime? DateTimeClosedAt { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
