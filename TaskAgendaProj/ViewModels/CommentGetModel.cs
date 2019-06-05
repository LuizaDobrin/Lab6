using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAgendaProj.Models;

namespace TaskAgendaProj.ViewModels
{
    public class CommentGetModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Important { get; set; }
        public int? TaskId { get; set; }



        public static CommentGetModel FromComment(Comment c)
        {
            return new CommentGetModel
            {
                Id = c.Id,
                Important = c.Important,
                Text = c.Text,
                TaskId = c.Task?.Id //evalueaza expresia daca comentariu.task este null returneaza null, altfel evalueaza expresia si returneaza valoarea
            };
        }
    }
}
