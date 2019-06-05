using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskAgendaProj.Models;
using TaskAgendaProj.ViewModels;


namespace TaskAgendaProj.Services
{
    public interface ICommentService
    {

        PaginatedList<CommentGetModel> GetAll(int page, string filterString);

    }

    public class CommentService : ICommentService
    {

        private TasksDbContext context;

        public CommentService(TasksDbContext context)
        {
            this.context = context;
        }

        public PaginatedList<CommentGetModel> GetAll(int page, string filterString)
        {

            IQueryable<Comment> result = context
                  .Comment
                  .Where(c => string.IsNullOrEmpty(filterString) || c.Text.Contains(filterString))
                  .OrderBy(c => c.Id)
                  .Include(c => c.Task);
           

            PaginatedList<CommentGetModel> paginatedResult = new PaginatedList<CommentGetModel>();
            paginatedResult.CurrentPage = page;

            paginatedResult.NumberOfPages = (result.Count() - 1) / PaginatedList<CommentGetModel>.EntriesPerPage + 1;
            result = result
                .Skip((page - 1) * PaginatedList<CommentGetModel>.EntriesPerPage)
                .Take(PaginatedList<CommentGetModel>.EntriesPerPage);
            paginatedResult.Entries = result.Select(f => CommentGetModel.FromComment(f)).ToList();

            return paginatedResult;
        }
    }
}
