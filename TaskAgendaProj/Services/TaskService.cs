using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskAgenda.ViewModels;
//using System.Threading.Tasks;
using TaskAgendaProj.Models;
using TaskAgendaProj.ViewModels;

namespace TaskAgendaProj.Services
{
    public interface ITaskService
    {

        PaginatedList<TaskGetModel> GetAll(int page, DateTime? from = null, DateTime? to = null);

        Task GetById(int id);

        Task Create(TaskPostModel task, User addedBy);

        Task Upsert(int id, Task task);

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

        public Task Upsert(int id, Task task)
        {
            var existing = context.Tasks.AsNoTracking().FirstOrDefault(e => e.Id == id);
            if (existing == null)
            {
                context.Tasks.Add(task);
                context.SaveChanges();
                return task;

            }

            task.Id = id;
            context.Tasks.Update(task);
            context.SaveChanges();
            return task;
        }

    }
}
