using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskAgendaProj.ViewModels;
//using System.Threading.Tasks;
using TaskAgendaProj.Models;


namespace TaskAgendaProj.Services
{
    public interface ITaskService
    {

        PaginatedList<TaskGetModel> GetAll(int page, DateTime? from = null, DateTime? to = null);

        Task GetById(int id);

        Task Create(TaskPostModel task, User addedBy);

        Task Upsert(int id, TaskPostModel task);

        Task Delete(int id);
       
    }
    public class TaskService : ITaskService
    {

        private TasksDbContext context;

        public TaskService(TasksDbContext context)
        {
            this.context = context;
        }


        public Task Create(TaskPostModel task, User addedBy)
        {
            
            Task toAdd = TaskPostModel.ToTask(task);
            toAdd.Owner = addedBy;
            context.Tasks.Add(toAdd);
            context.SaveChanges();
            return toAdd;
        }

        public Task Delete(int id)
        {
            var existing = context.Tasks
                //.Include(x => x.Comments)
                .FirstOrDefault(task => task.Id == id);
            if (existing == null)
            {
                return null;
            }
            ////o varianta de asterge comentariile unui task
            //foreach(var comment in existing.Coments)
            //{
            //    context.Coments.Remove(comment);
            //}

            context.Tasks.Remove(existing);
            context.SaveChanges();
            return existing;
        }

        public PaginatedList<TaskGetModel> GetAll(int page, DateTime? from = null, DateTime? to = null)
        {
            IQueryable<Task> result = context
                .Tasks
                .Include(x => x.Comments);
            PaginatedList<TaskGetModel> paginatedResult = new PaginatedList<TaskGetModel>();
            paginatedResult.CurrentPage = page;

            if (from != null)
            {
                result = result.Where(f => f.Deadline >= from);
            }
            if (to != null)
            {
                result = result.Where(f => f.Deadline <= to);
            }
            paginatedResult.NumberOfPages = (result.Count() - 1) / PaginatedList<TaskGetModel>.EntriesPerPage + 1;
            result = result
                .Skip((page - 1) * PaginatedList<TaskGetModel>.EntriesPerPage)
                .Take(PaginatedList<TaskGetModel>.EntriesPerPage);
            paginatedResult.Entries = result.Select(f => TaskGetModel.FromTask(f)).ToList();

            return paginatedResult;
        }

        public Task GetById(int id)
        {
            return context.Tasks
                .Include(x => x.Comments)
                .FirstOrDefault(e => e.Id == id);
        }

        public Task Upsert(int id, TaskPostModel task)
        {
            var existing = context.Tasks.AsNoTracking().FirstOrDefault(f => f.Id == id);
            if (existing == null)
            {
                Task toAdd = TaskPostModel.ToTask(task);
                context.Tasks.Add(toAdd);
                context.SaveChanges();
                return toAdd;
            }

            Task toUpdate = TaskPostModel.ToTask(task);
            toUpdate.Id = id;
            context.Tasks.Update(toUpdate);
            context.SaveChanges();
            return toUpdate;
        }
    }
}
