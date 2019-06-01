using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAgendaProj.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Important { get; set; }
        public Task Task { get; internal set; }
        public User Owner { get; set; }
    }
}
