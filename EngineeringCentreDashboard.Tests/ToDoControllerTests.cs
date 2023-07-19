using EngineeringCentreDashboard.Controllers;
using EngineeringCentreDashboard.Interfaces;
using EngineeringCentreDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineeringCentreDashboard.Tests
{
    public class ToDoControllerTests
    {
        private Mock<IToDoHelper> mockHelper;
        private ToDoController controller;
        private ToDo sampleToDo;

        public ToDoControllerTests()
        {
            mockHelper = new Mock<IToDoHelper>();
            controller = new ToDoController(mockHelper.Object);
            sampleToDo = new ToDo(1, "Test ToDo", "Sample description", DateTime.Now, 1);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithToDo()
        {
            // Arrange
            mockHelper.Setup(helper => helper.Get(It.IsAny<int>())).ReturnsAsync(sampleToDo);

            // Act
            var result = await controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(sampleToDo, okResult.Value);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenToDoDoesNotExist()
        {
            // Arrange
            mockHelper.Setup(helper => helper.Get(It.IsAny<int>())).ReturnsAsync((ToDo)null);

            // Act
            var result = await controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithToDoList()
        {
            // Arrange
            var toDoList = new List<ToDo> { sampleToDo };
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
            mockHelper.Setup(helper => helper.Update(It.IsAny<ToDo>())).ReturnsAsync(sampleToDo);

            // Act
            var result = await controller.Update(1, sampleToDo);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(sampleToDo, okResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenToDoDoesNotExist()
        {
            // Arrange
            mockHelper.Setup(helper => helper.Update(It.IsAny<ToDo>())).ReturnsAsync((ToDo)null);

            // Act
            var result = await controller.Update(1, sampleToDo);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdDoesNotMatchToDoId()
        {
            // Act
            var result = await controller.Update(2, sampleToDo);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Delete_ReturnsOkResult_WithToDoId()
        {
            // Act
            var result = controller.Delete(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(1, okResult.Value);
        }
    }

}
