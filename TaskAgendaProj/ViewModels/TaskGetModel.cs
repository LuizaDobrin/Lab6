using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
using TaskAgendaProj.Models;

namespace TaskAgendaProj.ViewModels
{
    public class TaskGetModel
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTimeAdded { get; set; }
        public DateTime Deadline { get; set; }
        public int NumberOfComments { get; set; }


        public static TaskGetModel FromTask(Task task)
        {
            return new TaskGetModel
            {
                Title = task.Title,                    //imi mapeaza functia asta pe fiecare element din result 
                Description = task.Description,        //(adica pe fiecare <Task>): title, description, dateTimeAdded
                DateTimeAdded = task.DateTimeAdded,
                Deadline = task.Deadline,                       //pt fiecare task t imi da un TaskGetModel, cu campurile completate aici (cele 3)
                NumberOfComments = task.Comments.Count           //cu count imi adun nr de comentarii. PT ASTA TRB SA AM PUS "INCLUDE"
                                                                 //altfel comments e null si va da eroare
            };
        }
    }
}
