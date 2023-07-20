using EngineeringCentreDashboard.Controllers;
using EngineeringCentreDashboard.Interfaces;
using EngineeringCentreDashboard.Models;
using EngineeringCentreDashboard.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EngineeringCentreDashboard.Tests
{
    public class ToDoControllerTests
    {
        private Mock<IToDoHelper> mockHelper;
        private ToDoController controller;
        private ToDoRequest sampleToDoRequest;
        private ToDo sampleToDo;

        public ToDoControllerTests()
        {
            mockHelper = new Mock<IToDoHelper>();
            controller = new ToDoController(mockHelper.Object);
            sampleToDoRequest = new ToDoRequest { Id = 1, Title = "Test ToDo", Description = "Sample description", DueDate = DateTime.Now, UserLoginId = 1 };
            sampleToDo = new ToDo { Id = 1, Title = "Test ToDo", Description = "Sample description", DueDate = DateTime.Now, UserLoginId = 1 };
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithToDo()
        {
            // Arrange
            mockHelper.Setup(helper => helper.Get(It.IsAny<int>())).ReturnsAsync(sampleToDoRequest);

            // Act
            var result = await controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(sampleToDoRequest, okResult.Value);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenToDoDoesNotExist()
        {
            // Arrange
            mockHelper.Setup(helper => helper.Get(It.IsAny<int>())).ReturnsAsync((ToDoRequest)null);

            // Act
            var result = await controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithToDoList()
        {
            // Arrange
            var toDoList = new List<ToDoRequest> { sampleToDoRequest };
            mockHelper.Setup(helper => helper.GetAll()).ReturnsAsync(toDoList);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(toDoList, okResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WithToDo()
        {
            // Arrange
            mockHelper.Setup(helper => helper.Update(It.IsAny<ToDoRequest>())).ReturnsAsync(sampleToDoRequest);

            // Act
            var result = await controller.Update(1, sampleToDoRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(sampleToDoRequest, okResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenToDoDoesNotExist()
        {
            // Arrange
            mockHelper.Setup(helper => helper.Update(It.IsAny<ToDoRequest>())).ReturnsAsync((ToDoRequest)null);

            // Act
            var result = await controller.Update(1, sampleToDoRequest);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdDoesNotMatchToDoId()
        {
            // Act
            var result = await controller.Update(2, sampleToDoRequest);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WithToDoId()
        {
            // Arrange
            mockHelper.Setup(helper => helper.Delete(It.IsAny<int>()));

            // Act
            var result = await controller.Delete(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, okResult.Value);
        }
    }
}

