using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TaskAgendaProj.Models;
using TaskAgendaProj.Services;
using TaskAgendaProj.ViewModels;

namespace WebApiTests
{
   public class TasksServiceTests
    {

        /// <summary>
        /// Test if a task is added corectly.
        /// </summary>
        [Test]
        public void ValidTaskShouldCreateANewTaskAndGetById()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(ValidTaskShouldCreateANewTaskAndGetById))
              .Options;

            using (var context = new TasksDbContext(options))
            {
                var taskService = new TaskService(context);

                var expected = taskService.Create(new TaskAgendaProj.ViewModels.TaskPostModel
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
                        new Comment()
                        {
                            Text = "Comment test for task",
                            Important = false,
                           // AddedBy = null
                        }
                    }
                }, null);

                var actual = taskService.GetById(expected.Id);

                Assert.IsNotNull(actual);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        /// Test if a task with invalid Type field is added.
        /// </summary>
        [Test]
        public void InvalidTypeOfTaskShouldNotCreatANewTask()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(InvalidTypeOfTaskShouldNotCreatANewTask))
              .Options;

            using (var context = new TasksDbContext(options))
            {
                var taskService = new TaskService(context);

                var expected = taskService.Create(new TaskAgendaProj.ViewModels.TaskPostModel
                {

                    Title = "task de test 1",
                    Description = "agfas",
                    DateTimeAdded = new DateTime(),
                    Deadline = new DateTime(),
                    Importance = "high",
                    Status = "in_progress",
                    DateTimeClosedAt = new DateTime(),
                    Comments = null
                }, null);

                var actual = taskService.GetById(expected.Id);

                Assert.IsNotNull(actual);
            }
        }



        [Test]
        public void GetAllShouldReturnCorrectNumberOfPages()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(GetAllShouldReturnCorrectNumberOfPages))
              .Options;

            using (var context = new TasksDbContext(options))
            {


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

                var allTasks = taskService.GetAll(1);
                Assert.NotNull(allTasks);
            }
        }

        [Test]
        public void CreateAndGetByIdTest()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(CreateAndGetByIdTest))
              .Options;

            using (var context = new TasksDbContext(options))
            {


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

                var taskCreated = taskService.GetById(addedTask.Id);
                Assert.NotNull(taskCreated);
                Assert.AreEqual(addedTask, taskCreated);
            }
        }

        [Test]
        public void DeleteTaskTest()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(DeleteTaskTest))
              .Options;

            using (var context = new TasksDbContext(options))
            {


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

                var taskDeleted = taskService.Delete(addedTask.Id);
                Assert.IsNotNull(taskDeleted);
            }
        }

        [Test]
        public void DeleteAValidTask()
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(DeleteAValidTask))
              .Options;

            using (var context = new TasksDbContext(options))
            {
                var taskService = new TaskService(context);
                var expected = taskService.Create(new TaskPostModel
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


                var taskDeleted = taskService.Delete(expected.Id);
                var taskDeletedNull = taskService.Delete(33);
                Assert.IsNotNull(taskDeleted);
                Assert.IsNull(taskDeletedNull);
            }
        }
        //[Test]
        //public void UpdateTest1()
        //{
        //    var options = new DbContextOptionsBuilder<TasksDbContext>()
        //      .UseInMemoryDatabase(databaseName: nameof(UpdateTest1))
        //      .Options;

        //    using (var context = new TasksDbContext(options))
        //    {


        //        var taskService = new TaskService(context);

        //        var addedTask = taskService.Create(new TaskAgendaProj.ViewModels.TaskPostModel
        //        {
        //            Title = "task de test 1",
        //            Description = "agfas",
        //            DateTimeAdded = new DateTime(),
        //            Deadline = new DateTime(),
        //            Importance = "high",
        //            Status = "in_progress",
        //            DateTimeClosedAt = new DateTime(),
        //            Comments = new List<Comment>()
        //            {
        //                new Comment
        //                {
        //                    Important = true,
        //                    Text = "asd",
        //                    Owner = null
        //                }
        //            },

        //        }, null);
        //        var addedTaskForUpdate = new Task
        //        {
        //            Title = "updated",
        //            Description = "agkfksjd",
        //            DateTimeAdded = new DateTime(),
        //            Deadline = new DateTime(),
        //            Importance = "medium",
        //            Status = "in_progress",
        //            DateTimeClosedAt = new DateTime(),

        //            Comments = new List<Comment>()
        //            {
        //                new Comment
        //                {
        //                    Important = true,
        //                    Text = "asd",
        //                    Owner = null
        //                }
        //            },
        //            Owner = null
        //        };




        //        var updateResult = taskService.Upsert(addedTask.Id, addedTaskForUpdate);
        //        var updateResultNull = taskService.Upsert(2, addedTaskForUpdate);
        //        Assert.IsNull(updateResultNull);

        //    }
        //}


    }
}
