using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Timelogger.Commands;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;
using Xunit;


namespace Timelogger.Aplication.Test
{

    public class GetAllProjectsHandlerTests
    {
        [Fact]
        public async Task GetAllProjectsHandler_SortAsc_ReturnsSortedProjectsInAscendingOrder()
        {
            // Arrange
            var mockService = new Mock<IProjectService>();
            var projects = new List<Project>()
            {
                new Project { Id = "1", Name = "Project 1", DeadLine = DateTime.Today.AddDays(2), TimePerWeek = 40, IsCompleted = false },
                new Project { Id = "2", Name = "Project 2", DeadLine = DateTime.Today.AddDays(1), TimePerWeek = 20, IsCompleted = false },
                new Project { Id = "3", Name = "Project 3", DeadLine = DateTime.Today.AddDays(3), TimePerWeek = 30, IsCompleted = true }
            };
            mockService.Setup(x => x.GetAllProjects(false, It.IsAny<CancellationToken>())).ReturnsAsync(projects);

            var handler = new GetAllProjectsHandler(mockService.Object);
            var request = new GetAllProjectsCommand { IsToSortDesc = false };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Data.Count);
            Assert.Equal("Project 2", result.Data[0].Name);
            Assert.Equal("Project 1", result.Data[1].Name);
            Assert.Equal("Project 3", result.Data[2].Name);
        }


        [Fact]
        public async Task GetAllProjectsHandler_SortDesc_ReturnsSortedProjectsInDescendingOrder()
        {
            // Arrange
            var mockService = new Mock<IProjectService>();
            var projects = new List<Project>()
            {
                new Project { Id = "1", Name = "Project 1", DeadLine = DateTime.Today.AddDays(2), TimePerWeek = 40, IsCompleted = false },
                new Project { Id = "2", Name = "Project 2", DeadLine = DateTime.Today.AddDays(1), TimePerWeek = 20, IsCompleted = false },
                new Project { Id = "3", Name = "Project 3", DeadLine = DateTime.Today.AddDays(3), TimePerWeek = 30, IsCompleted = true }
            };
            mockService.Setup(x => x.GetAllProjects(false, It.IsAny<CancellationToken>())).ReturnsAsync(projects);

            var handler = new GetAllProjectsHandler(mockService.Object);
            var request = new GetAllProjectsCommand { IsToSortDesc = true };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Data.Count);
            Assert.Equal("Project 3", result.Data[0].Name);
            Assert.Equal("Project 1", result.Data[1].Name);
            Assert.Equal("Project 2", result.Data[2].Name);
        }

    }
}

