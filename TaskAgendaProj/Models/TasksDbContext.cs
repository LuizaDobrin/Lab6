using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAgendaProj.Models
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity => {
                entity.HasIndex(u => u.Username).IsUnique();            //pune un index pe coloana username
            });

            builder.Entity<Comment>()
                .HasOne(t => t.Task)
                .WithMany(c => c.Comments)
                .OnDelete(DeleteBehavior.Cascade);

            // builder.Entity<Task>()
            //.HasOne(e => e.Owner)
            //.WithMany(c => c.Tasks)
            //.OnDelete(DeleteBehavior.Cascade);
        

    }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserUserRole> UserUserRole { get; set; }
    }
}
