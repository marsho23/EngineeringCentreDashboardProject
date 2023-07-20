using EngineeringCentreDashboard.Business;
using EngineeringCentreDashboard.Data;
using EngineeringCentreDashboard.Interfaces;
using EngineeringCentreDashboard.Models;
using EngineeringCentreDashboard.Models.Request;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EngineeringCentreDashboard.Tests
{
    public class ToDoHelperTests
    {
        [Fact]
        public async Task AddToDoTest()
        {
            var options = new DbContextOptionsBuilder<EngineeringDashboardDbContext>()
                .UseInMemoryDatabase(databaseName: "ToDoDatabase_Add")
                .Options;

            using (var context = new EngineeringDashboardDbContext(options))
            {
                var userLogin = new UserLogin { Id = 1, Username = "testuser", Password = "password", Email = "testuser@example.com" };
                context.UserLogins.Add(userLogin);
                await context.SaveChangesAsync();

                var testToDoRequest = new ToDoRequest { Title = "Test ToDo Add", Description = "Sample description for Add Test", DueDate = DateTime.Now, UserLoginId = userLogin.Id };

                var toDoHelper = new ToDoHelper(context);
                var toDo = await toDoHelper.Add(testToDoRequest);

                Assert.NotNull(toDo);
                Assert.Equal(1, toDo.Id);
                Assert.Equal(testToDoRequest.Title, toDo.Title);
                Assert.Equal(testToDoRequest.Description, toDo.Description);
                Assert.True((testToDoRequest.DueDate - toDo.DueDate).TotalSeconds < 1);
                Assert.Equal(testToDoRequest.UserLoginId, toDo.UserLoginId);
            }

            using (var context = new EngineeringDashboardDbContext(options))
            {
                var toDo = await context.ToDoItems.FirstOrDefaultAsync();
                Assert.NotNull(toDo);
                Assert.Equal(1, toDo.Id);
                Assert.Equal("Test ToDo Add", toDo.Title);
                Assert.Equal("Sample description for Add Test", toDo.Description);
                Assert.True((DateTime.Now - toDo.DueDate).TotalSeconds < 1);
                Assert.Equal(1, toDo.UserLoginId);
            }
        }



        [Fact]
        public async Task GetToDoTest()
        {
            var options = new DbContextOptionsBuilder<EngineeringDashboardDbContext>()
                .UseInMemoryDatabase(databaseName: "ToDoDatabase_Get")
                .Options;

            using (var context = new EngineeringDashboardDbContext(options))
            {
                var userLogin = new UserLogin { Id = 1, Username = "testuser", Password = "password", Email = "testuser@example.com" };
                context.UserLogins.Add(userLogin);
                await context.SaveChangesAsync();

                var testToDo = new ToDo { Id = 1, Title = "Test ToDo Get", Description = "Sample description for Get Test", DueDate = DateTime.Now, UserLoginId = userLogin.Id };
                context.ToDoItems.Add(testToDo);
                await context.SaveChangesAsync();
            }

            using (var context = new EngineeringDashboardDbContext(options))
            {
                var toDoHelper = new ToDoHelper(context);
                var toDo = await toDoHelper.Get(1);

                Assert.NotNull(toDo);
                Assert.Equal(1, toDo.Id);
                Assert.Equal("Test ToDo Get", toDo.Title);
                Assert.Equal("Sample description for Get Test", toDo.Description);
                Assert.True((DateTime.Now - toDo.DueDate).TotalSeconds < 1);
                Assert.Equal(1, toDo.UserLoginId);
            }
        }

        [Fact]
        public async Task GetAllToDosTest()
        {
            var options = new DbContextOptionsBuilder<EngineeringDashboardDbContext>()
                .UseInMemoryDatabase(databaseName: "ToDoDatabase_GetAll")
                .Options;

            using (var context = new EngineeringDashboardDbContext(options))
            {
                var userLogin = new UserLogin { Id = 1, Username = "testuser", Password = "password", Email = "testuser@example.com" };
                context.UserLogins.Add(userLogin);
                await context.SaveChangesAsync();

                context.ToDoItems.Add(new ToDo { Id = 1, Title = "Test ToDo 1", Description = "Sample description", DueDate = DateTime.Now, UserLoginId = userLogin.Id });
                context.ToDoItems.Add(new ToDo { Id = 2, Title = "Test ToDo 2", Description = "Sample description", DueDate = DateTime.Now, UserLoginId = userLogin.Id });
                await context.SaveChangesAsync();
            }

            using (var context = new EngineeringDashboardDbContext(options))
            {
                var toDoHelper = new ToDoHelper(context);
                var toDos = await toDoHelper.GetAll();

                Assert.Equal(2, toDos.Count());

                var firstToDo = toDos.First();
                Assert.Equal(1, firstToDo.Id);
                Assert.Equal("Test ToDo 1", firstToDo.Title);
                Assert.Equal("Sample description", firstToDo.Description);
                Assert.True((DateTime.Now - firstToDo.DueDate).TotalSeconds < 1);
                Assert.Equal(1, firstToDo.UserLoginId);

                var secondToDo = toDos.Skip(1).First();
                Assert.Equal(2, secondToDo.Id);
                Assert.Equal("Test ToDo 2", secondToDo.Title);
                Assert.Equal("Sample description", secondToDo.Description);
                Assert.True((DateTime.Now - secondToDo.DueDate).TotalSeconds < 1);
                Assert.Equal(1, secondToDo.UserLoginId);
            }
        }

        [Fact]
        public async Task UpdateToDoTest()
        {
            var options = new DbContextOptionsBuilder<EngineeringDashboardDbContext>()
                .UseInMemoryDatabase(databaseName: "ToDoDatabase_Update")
                .Options;

            using (var context = new EngineeringDashboardDbContext(options))
            {
                var userLogin = new UserLogin { Id = 1, Username = "testuser", Password = "password", Email = "testuser@example.com" };
                context.UserLogins.Add(userLogin);
                await context.SaveChangesAsync();

                var testToDo = new ToDo { Id = 1, Title = "Test ToDo Update", Description = "Sample description for Update Test", DueDate = DateTime.Now, UserLoginId = userLogin.Id };
                context.ToDoItems.Add(testToDo);
                await context.SaveChangesAsync();
            }

            using (var context = new EngineeringDashboardDbContext(options))
            {
                var toDoHelper = new ToDoHelper(context);
                var updatedToDoRequest = new ToDoRequest { Id = 1, Title = "Updated ToDo", Description = "Updated description", DueDate = DateTime.Now.AddDays(1), UserLoginId = 1 };
                var updatedToDo = await toDoHelper.Update(updatedToDoRequest);

                Assert.Equal(1, updatedToDo.Id);
                Assert.Equal("Updated ToDo", updatedToDo.Title);
                Assert.Equal("Updated description", updatedToDo.Description);
                Assert.True((DateTime.Now.AddDays(1) - updatedToDo.DueDate).TotalSeconds < 1);
                Assert.Equal(1, updatedToDo.UserLoginId);
            }

            using (var context = new EngineeringDashboardDbContext(options))
            {
                var toDo = await context.ToDoItems.FindAsync(1);
                Assert.Equal("Updated ToDo", toDo.Title);
                Assert.Equal("Updated description", toDo.Description);
                Assert.True((DateTime.Now.AddDays(1) - toDo.DueDate).TotalSeconds < 1);
                Assert.Equal(1, toDo.UserLoginId);
            }
        }

        [Fact]
        public async Task DeleteToDoTest()
        {
            var options = new DbContextOptionsBuilder<EngineeringDashboardDbContext>()
                .UseInMemoryDatabase(databaseName: "ToDoDatabase_Delete")
                .Options;

            using (var context = new EngineeringDashboardDbContext(options))
            {
                var userLogin = new UserLogin { Id = 1, Username = "testuser", Password = "password", Email = "testuser@example.com" };
                context.UserLogins.Add(userLogin);
                await context.SaveChangesAsync();

                var testToDo = new ToDo { Id = 1, Title = "Test ToDo Delete", Description = "Sample description for Delete Test", DueDate = DateTime.Now, UserLoginId = userLogin.Id };
                context.ToDoItems.Add(testToDo);
                await context.SaveChangesAsync();
            }

            using (var context = new EngineeringDashboardDbContext(options))
            {
                var toDoHelper = new ToDoHelper(context);
                await toDoHelper.Delete(1);

                var toDo = await context.ToDoItems.FindAsync(1);
                Assert.Null(toDo);
            }
        }

    }
}


