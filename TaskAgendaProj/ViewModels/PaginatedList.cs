using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAgendaProj.ViewModels
{
    public class PaginatedList<T>
    {
        public const int EntriesPerPage = 5;            //max 5 intrari pe pagina
        public int CurrentPage { get; set; }            //numarul pag pe care ma aflu
        public int NumberOfPages { get; set; }
        public List<T> Entries { get; set; }
    }
}
