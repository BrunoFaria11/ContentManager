using System;
using System.Threading.Tasks;
using Moq;
using Timelogger.Application.Common.Exceptions;
using Timelogger.Commands;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;
using Xunit;


namespace Timelogger.Aplication.Test
{
    public class UpdateProjectsHandlerTests
    {
        private readonly Mock<IProjectService> _projectServiceMock;
        private readonly UpdateProjectsHandler _updateProjectsHandler;

        public UpdateProjectsHandlerTests()
        {
            _projectServiceMock = new Mock<IProjectService>();
            _updateProjectsHandler = new UpdateProjectsHandler(_projectServiceMock.Object);
        }

        [Fact]
        public async Task Handle_WhenProjectAlreadyExists_ThrowsApiException()
        {
            // Arrange
            var request = new UpdateProjectsCommand
            {
                Id ="1", 
                Name = "Existing Project",
                DeadLine = new DateTime(2023, 4, 30),
                TimePerWeek = 40,
                IsCompleted = false
            };
            var existingProject = new Project
            {
                Id = "2",
                Name = "Existing Project",
                DeadLine = new DateTime(2023, 5, 31),
                TimePerWeek = 20,
                IsCompleted = false
            };
            _projectServiceMock.Setup(s => s.GetProjectByName(request.Name, default))
                .ReturnsAsync(existingProject);

            // Act & Assert
            await Assert.ThrowsAsync<ApiException>(() => _updateProjectsHandler.Handle(request, default));
        }

        [Fact]
        public async Task Handle_WhenProjectDoesNotExist_ReturnsUpdatedProject()
        {
            // Arrange
            var request = new UpdateProjectsCommand
            {
                Id ="1", 
                Name = "New Project",
                DeadLine = new DateTime(2023, 4, 30),
                TimePerWeek = 40,
                IsCompleted = false
            };
            var existingProject = new Project
            {
                Id ="1", 
                Name = "Existing Project",
                DeadLine = new DateTime(2023, 5, 31),
                TimePerWeek = 20,
                IsCompleted = false
            };
            var updatedProject = new Project
            {
                Id ="1", 
                Name = "New Project",
                DeadLine = new DateTime(2023, 4, 30),
                TimePerWeek = 40,
                IsCompleted = false
            };
            _projectServiceMock.Setup(s => s.GetProjectByName(request.Name, default))
                .ReturnsAsync((Project)null);
            _projectServiceMock.Setup(s => s.GetProject(request.Id, default))
                .ReturnsAsync(existingProject);
            _projectServiceMock.Setup(s => s.UpdateProject(updatedProject, default))
                .ReturnsAsync(updatedProject);

            // Act
            var response = await _updateProjectsHandler.Handle(request, default);

            // Assert
            Assert.Equal(updatedProject, response.Data);
        }
    }

}

