using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Linq;
using TaskAgendaProj.Models;
using TaskAgendaProj.Services;

namespace Tests
{
    public class UsersServiceTests
    {
        private IOptions<AppSettings> config;

        [SetUp]
        public void Setup()
        {
            config = Options.Create(new AppSettings
            {
                Secret = "dsadhjcghduihdfhdifd8ih"
            });
        }

        /// <summary>
        /// TODO: AAA - Arrange, Act, Assert
        /// </summary>
        [Test]
        public void ValidRegisterShouldCreateANewUser() //daca dau date vreau sa creeze un nou user
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(ValidRegisterShouldCreateANewUser))// "ValidRegisterShouldCreateANewUser")
              .Options;

            using (var context = new TasksDbContext(options))    //creaza un DbContext dar InMemory
            {
                var usersService = new UsersService(context, config);
                var added = new TaskAgendaProj.ViewModels.RegisterPostModel
                {
                    Email = "a@a.b",
                    FirstName = "fdsfsdfs",
                    LastName = "fdsfs",
                    Password = "1234567",
                    Username = "test_username"
                };
                var result = usersService.Register(added);

                Assert.IsNotNull(result);
                Assert.AreEqual(added.Username, result.Username);
            }
        }

        [Test]
        public void GetAllShouldReturnAllRegisteredUsers() 
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(GetAllShouldReturnAllRegisteredUsers))
              .Options;

            using (var context = new TasksDbContext(options))   
            {
                var usersService = new UsersService(context, config);
                var added = new TaskAgendaProj.ViewModels.RegisterPostModel
                {
                    Email = "UserTest@test.com",
                    FirstName = "FirstNameTest",
                    LastName = "LastNameTest",
                    Password = "1234567",
                    Username = "test_username"
                };
                var newadded = new TaskAgendaProj.ViewModels.RegisterPostModel
                {
                    Email = "UserTest2@test.com",
                    FirstName = "FirstNameTest2",
                    LastName = "LastNameTest2",
                    Password = "1234567890",
                    Username = "test_username2"
                };
                usersService.Register(added);
                usersService.Register(newadded);
                int number = usersService.GetAll().Count();

                Assert.IsNotNull(number);
                Assert.AreEqual(2,number);
            }
        }

        [Test]
        public void AuthenticateShouldLogTheUser() 
        {
            var options = new DbContextOptionsBuilder<TasksDbContext>()
              .UseInMemoryDatabase(databaseName: nameof(AuthenticateShouldLogTheUser))
              .Options;

            using (var context = new TasksDbContext(options))
            {
                var usersService = new UsersService(context, config);
                var added = new TaskAgendaProj.ViewModels.RegisterPostModel
                {
                    Email = "UserTest3@test.com",
                    FirstName = "FirstNameTest3",
                    LastName = "LastNameTest3",
                    Password = "12345678",
                    Username = "test_username2"
                };
                var result = usersService.Register(added);

                var authenticate=new TaskAgendaProj.ViewModels.LoginPostModel
                {
                    Username = "test_username2",
                    Password = "12345678",
                };

                var authenticateresult = usersService.Authenticate(added.Username, added.Password);

                Assert.IsNotNull(authenticateresult);
                Assert.AreEqual(1, authenticateresult.Id);
                Assert.AreEqual(authenticate.Username,authenticateresult.Username);
            }
        }


    }
}