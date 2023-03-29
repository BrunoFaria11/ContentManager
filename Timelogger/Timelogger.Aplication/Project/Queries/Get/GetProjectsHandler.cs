using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Timelogger.Application.Common.Models;
using Timelogger.Commands;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;
using Xunit;


namespace Timelogger.Aplication.Test
{
    public class GetProjectsHandlerTests
    {
        [Fact]
        public async Task GetProjectsHandler_ReturnsResponseWithProject()
        {
            // Arrange
            var project = new Project()
            {
                Id = "1",
                Name = "Test Project",
                DeadLine = DateTime.Now.AddDays(30),
                TimePerWeek = 20,
                IsCompleted = false
            };
            var mockProjectService = new Mock<IProjectService>();
            mockProjectService.Setup(x => x.GetProject(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(project);
            var handler = new GetProjectsHandler(mockProjectService.Object);

            // Act
            var result = await handler.Handle(new GetProjectsCommand() { Id = "1" }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Response<Project>>(result);
            Assert.Equal(project.Id, result.Data.Id);
            Assert.Equal(project.Name, result.Data.Name);
            Assert.Equal(project.DeadLine, result.Data.DeadLine);
            Assert.Equal(project.TimePerWeek, result.Data.TimePerWeek);
            Assert.Equal(project.IsCompleted, result.Data.IsCompleted);
        }
    }

}

