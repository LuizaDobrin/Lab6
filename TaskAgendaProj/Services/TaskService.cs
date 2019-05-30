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

        IEnumerable<TaskGetModel> GetAll(DateTime? from = null, DateTime? to = null);

        Task GetById(int id);

        Task Create(TaskPostModel user);

        Task Upsert(int id, Task user);

        Task Delete(int id);
       
    }
    public class TaskService : ITaskService
    {

        private TasksDbContext context;

        public TaskService(TasksDbContext context)
        {
            this.context = context;
        }

        public Task Create(TaskPostModel task)
        {
        Task addTa = TaskPostModel.ToTask(task);
        context.Tasks.Add(addTa);
        context.SaveChanges();
        return addTa;
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

        public IEnumerable<TaskGetModel> GetAll(DateTime? from = null, DateTime? to = null)
        {
            IQueryable<Task> result = context
                .Tasks
                .Include(x => x.Comments);
            if ((from == null && to == null))

            {
                return result.Select(t => TaskGetModel.FromTask(t));
            }
            if (from != null)
            {
                result = result.Where(e => e.Deadline >= from);
            }
            if (to != null)
            {
                result = result.Where(e => e.Deadline <= to);
            }
            return result.Select(t => TaskGetModel.FromTask(t));
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
