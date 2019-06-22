using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAgendaProj.Models;

namespace TaskAgendaProj.ViewModels
{
    public class CommentPostModel
    {

        public string Text { get; set; }
        public bool Important { get; set; }
        public int TaskId { get; set; }

        public static Comment ToComment(CommentPostModel comment)
        {
            return new Comment
            {
                Text = comment.Text,
                Important = comment.Important,


            };
        }
    
}
}
