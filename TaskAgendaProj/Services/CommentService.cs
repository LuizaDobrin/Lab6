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

        public Comment GetById(int id)
        {
            return context.Comment
                // .Include(e => e.Comments)
                .FirstOrDefault(e => e.Id == id);
        }
        public Comment Create(CommentPostModel comment, int id)
        {
            Comment toAdd = CommentPostModel.ToComment(comment);
            Task task = context.Tasks.FirstOrDefault(tas => tas.Id == id);
            task.Comments.Add(toAdd);
            context.SaveChanges();
            return toAdd;


        }

        public Comment Delete(int id)
        {
            var existing = context.Comment.FirstOrDefault(comment => comment.Id == id);
            if (existing == null)
            {
                return null;
            }
            context.Comment.Remove(existing);
            context.SaveChanges();
            return existing;
        }
        //public comment upsert(int id, comment task)
        //{
        //    var existing = context.comments.asnotracking().firstordefault(f => f.id == id);
        //    if (existing == null)
        //    {
        //        context.comments.add(task);
        //        context.savechanges();
        //        return task;
        //    }
        //    task.id = id;
        //    context.comments.update(task);
        //    context.savechanges();
        //    return task;
        //}
  
    }
}
