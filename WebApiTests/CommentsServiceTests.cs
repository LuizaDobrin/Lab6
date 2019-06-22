using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TaskAgendaProj.Models;
using TaskAgendaProj.Services;

namespace WebApiTests
{
    public class CommentsServiceTests
    {
        [Test]
        public void GetAllShouldReturnCorrectNumberOfPages()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(GetAllShouldReturnCorrectNumberOfPages))
              .Options;

            using (var context = new TasksDbContext(options))
            {

                var commentService = new CommentService(context);
                var taskService = new TaskService(context);
                var addedTask = taskService.Create(new TaskAgendaProj.ViewModels.TaskPostModel
                {

                    Title = "task de test 1",
                    Description = "agfas",
                    DateTimeAdded = new DateTime(),
                    Deadline = new DateTime(),
                    Importance = "high",
                    Status = "in_progress",
                    DateTimeClosedAt = new DateTime(),
                    Comments = new List<Comment>()
                    {
                        new Comment
                        {
                            Important = true,
                            Text = "bla",
                            Owner = null
                        }
                    },

                }, null);

                var allComments = commentService.GetAll(1, string.Empty);
                Assert.AreEqual(allComments.NumberOfPages, 1);
            }
        }

  [Test]
        public void GetByIdTest()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(GetByIdTest))
              .Options;

            using (var context = new TasksDbContext(options))
            {

                var commentService = new CommentService(context);
                var taskService = new TaskService(context);
                var addedTask = taskService.Create(new TaskAgendaProj.ViewModels.TaskPostModel
                {
                    Title = "task de test 1",
                    Description = "agfas",
                    DateTimeAdded = new DateTime(),
                    Deadline = new DateTime(),
                    Importance = "high",
                    Status = "in_progress",
                    DateTimeClosedAt = new DateTime(),

                    Comments = new List<Comment>()
                    {
                        new Comment
                        {
                            Important = true,
                            Text = "asd",
                            Owner = null
                        }
                    },

                }, null);

                var addedComment = commentService.Create(new TaskAgendaProj.ViewModels.CommentPostModel
                {
                    Important = true,
                    Text = "asd",
                }, addedTask.Id);

                var comment = commentService.GetById(addedComment.Id);
                Assert.NotNull(comment);
            }
        }

        [Test]
        public void DeleteTest()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(DeleteTest))
              .Options;

            using (var context = new TasksDbContext(options))
            {

                var commentService = new CommentService(context);
                var taskService = new TaskService(context);
                var addedTask = taskService.Create(new TaskAgendaProj.ViewModels.TaskPostModel
                {
                    Title = "task de test 1",
                    Description = "agfas",
                    DateTimeAdded = new DateTime(),
                    Deadline = new DateTime(),
                    Importance = "high",
                    Status = "in_progress",
                    DateTimeClosedAt = new DateTime(),

                    Comments = new List<Comment>()
                    {
                        new Comment
                        {
                            Important = true,
                            Text = "asd",
                            Owner = null
                        }
                    },

                }, null);

                var addedComment = commentService.Create(new TaskAgendaProj.ViewModels.CommentPostModel
                {
                    Important = true,
                    Text = "fdlkflsdkm",
                }, addedTask.Id);

                var comment = commentService.Delete(addedComment.Id);
                var commentNull = commentService.Delete(17);
                Assert.IsNull(commentNull);
                Assert.NotNull(comment);
            }
        }




    }
}
    

