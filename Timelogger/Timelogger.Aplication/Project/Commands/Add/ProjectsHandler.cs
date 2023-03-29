using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Timelogger.Application.Common.Exceptions;
using Timelogger.Application.Common.Models;
using Timelogger.Commands;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;
using Xunit;


namespace Timelogger.Aplication.Test
{
    public class ProjectsHandlerTests
    {
        private readonly Mock<IProjectService> _projectServiceMock;

        public ProjectsHandlerTests()
        {
            _projectServiceMock = new Mock<IProjectService>();
        }

        [Fact]
        public async Task Handle_ProjectDoesNotExist_ShouldReturnResponseWithNewProject()
        {
            // Arrange
            var command = new ProjectsCommand
            {
                Name = "New Project",
                DeadLine = new DateTime(2023, 12, 31),
                TimePerWeek = 40
            };

            var newProject = new Project
            {
                Name = command.Name,
                DeadLine = command.DeadLine,
                TimePerWeek = command.TimePerWeek
            };

            _projectServiceMock
                .Setup(service => service.GetProjectByName(command.Name, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Project)null);

            _projectServiceMock
                .Setup(service => service.AddProject(newProject, It.IsAny<CancellationToken>()))
                .ReturnsAsync(newProject);

            var handler = new ProjectsHandler(_projectServiceMock.Object);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsType<Response<Project>>(response);
            Assert.Equal(newProject, response.Data);

            _projectServiceMock.Verify(service => service.GetProjectByName(command.Name, It.IsAny<CancellationToken>()), Times.Once);
            _projectServiceMock.Verify(service => service.AddProject(newProject, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ProjectAlreadyExists_ShouldThrowApiException()
        {
            // Arrange
            var command = new ProjectsCommand
            {
                Name = "Existing Project",
                DeadLine = new DateTime(2023, 12, 31),
                TimePerWeek = 40
            };

            var existingProject = new Project
            {
                Name = command.Name,
                DeadLine = command.DeadLine,
                TimePerWeek = command.TimePerWeek
            };

            _projectServiceMock
                .Setup(service => service.GetProjectByName(command.Name, It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingProject);

            var handler = new ProjectsHandler(_projectServiceMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ApiException>(() => handler.Handle(command, CancellationToken.None));

            _projectServiceMock.Verify(service => service.GetProjectByName(command.Name, It.IsAny<CancellationToken>()), Times.Once);
            _projectServiceMock.Verify(service => service.AddProject(It.IsAny<Project>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}

