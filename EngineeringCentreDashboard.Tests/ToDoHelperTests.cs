using EngineeringCentreDashboard.Business;
using EngineeringCentreDashboard.Data;
using EngineeringCentreDashboard.Interfaces;
using EngineeringCentreDashboard.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EngineeringCentreDashboard.Tests
{
    public class ToDoHelperTests
    {
        [Fact]
        public async void GetAllToDosTest()
        {
            var options = new DbContextOptionsBuilder<ToDoDbContext>()
                .UseInMemoryDatabase(databaseName: "ToDoDatabase_GetAll")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new ToDoDbContext(options))
            {
                //var mockToDoItem = new ToDo(1, "Test ToDo", "Sample description", DateTime.Now);
                context.ToDoItems.Add(new ToDo(1, "Test ToDo 1", "Sample description", DateTime.Now));
                context.ToDoItems.Add(new ToDo(2, "Test ToDo 2", "Sample description", DateTime.Now));
                await context.SaveChangesAsync();
            }

            // Use a clean instance of the context to run the test
            using (var context = new ToDoDbContext(options))
            {
                var toDoHelper = new ToDoHelper(context);
                var toDos = await toDoHelper.GetAll();

                Assert.Equal(2, toDos.Count());
            }
        }

        [Fact]
        public async void GetToDoTest()
        {
            var options = new DbContextOptionsBuilder<ToDoDbContext>()
                .UseInMemoryDatabase(databaseName: "ToDoDatabase_Get")
                .Options;

            using (var context = new ToDoDbContext(options))
            {
                var testToDo = new ToDo(1, "Test ToDo Get", "Sample description for Get Test", DateTime.Now);
                context.ToDoItems.Add(testToDo);
                await context.SaveChangesAsync();
            }

            using (var context = new ToDoDbContext(options))
            {
                var toDoHelper = new ToDoHelper(context);
                var toDo = await toDoHelper.Get(1);

                Assert.NotNull(toDo);
                Assert.Equal(1, toDo.Id);
            }
        }

        [Fact]
        public async void AddToDoTest()
        {
            var options = new DbContextOptionsBuilder<ToDoDbContext>()
                .UseInMemoryDatabase(databaseName: "ToDoDatabase_Add")
                .Options;

            var testToDo = new ToDo(1, "Test ToDo Add", "Sample description for Add Test", DateTime.Now,true);

            using (var context = new ToDoDbContext(options))
            {
                var toDoHelper = new ToDoHelper(context);
                var toDo = await toDoHelper.Add(testToDo);

                //Assert.Equal(1, toDo.Id);
                Assert.NotNull(toDo);
                Assert.Equal(testToDo.Id, toDo.Id);
                Assert.Equal(testToDo.Title, toDo.Title);
                Assert.Equal(testToDo.Description, toDo.Description);
                Assert.Equal(testToDo.IsCompleted, toDo.IsCompleted);
                Assert.Equal(testToDo.DueDate, toDo.DueDate);
            }

            using (var context = new ToDoDbContext(options))
            {
                var toDo = await context.ToDoItems.FirstOrDefaultAsync();
                Assert.NotNull(toDo);
                Assert.Equal(testToDo.Id, toDo.Id);
                Assert.Equal(testToDo.Title, toDo.Title);
                Assert.Equal(testToDo.Description, toDo.Description);
                Assert.Equal(testToDo.IsCompleted, toDo.IsCompleted);
                Assert.Equal(testToDo.DueDate, toDo.DueDate);
            }
        }

        [Fact]
        public async void UpdateToDoTest()
        {
            var options = new DbContextOptionsBuilder<ToDoDbContext>()
                .UseInMemoryDatabase(databaseName: "ToDoDatabase_Update")
                .Options;

            var testToDo = new ToDo(1, "Test ToDo Update", "Sample description for Update Test", DateTime.Now);

            using (var context = new ToDoDbContext(options))
            {
                context.ToDoItems.Add(testToDo);
                await context.SaveChangesAsync();
            }

            testToDo.Title = "Updated ToDo";
            testToDo.IsCompleted = true;

            using (var context = new ToDoDbContext(options))
            {
                var toDoHelper = new ToDoHelper(context);
                var updatedToDo = await toDoHelper.Update(testToDo);

                Assert.Equal("Updated ToDo", updatedToDo.Title);
                Assert.True(updatedToDo.IsCompleted);
            }

            using (var context = new ToDoDbContext(options))
            {
                var toDo = await context.ToDoItems.FindAsync(1);
                Assert.Equal("Updated ToDo", toDo.Title);
                Assert.True(toDo.IsCompleted);
            }
        }

        [Fact]
        public async void DeleteToDoTest()
        {
            var options = new DbContextOptionsBuilder<ToDoDbContext>()
                .UseInMemoryDatabase(databaseName: "ToDoDatabase_Delete")
                .Options;

            using (var context = new ToDoDbContext(options))
            {
                var testToDo = new ToDo(1, "Test ToDo Delete", "Sample description for Delete Test", DateTime.Now,true);
                context.ToDoItems.Add(testToDo);
                await context.SaveChangesAsync();
            }

            using (var context = new ToDoDbContext(options))
            {
                var toDoHelper = new ToDoHelper(context);
                await toDoHelper.Delete(1);

                Assert.Null(await context.ToDoItems.FindAsync(1));
            }
        }


    }
}
