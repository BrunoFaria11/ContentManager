using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Timelogger.Common.Interfaces.Repositories;
using Timelogger.Common.Services;
using Timelogger.Entities;
using Xunit;

namespace Timelogger.Aplication.Test
{
    public class ProjectServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ProjectService _projectService;

        public ProjectServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _projectService = new ProjectService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task AddProject_ShouldCallAddAsyncAndSaveChangesAsync()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var project = new Project();

            // Act
            var result = await _projectService.AddProject(project, cancellationToken);

            // Assert
            _unitOfWorkMock.Verify(x => x.ProjectRepository.AddAsync(project, cancellationToken), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(cancellationToken), Times.Once);
            Assert.Equal(result, project);
        }

        [Fact]
        public async Task GetAllProjects_ShouldCallFindAllAsync()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var isCompleted = true;

            // Act
            var result = await _projectService.GetAllProjects(isCompleted, cancellationToken);

            // Assert
            _unitOfWorkMock.Verify(x => x.ProjectRepository.FindAllAsync(
                It.IsAny<Expression<Func<Project, bool>>>(), cancellationToken), Times.Once);
            Assert.IsType<List<Project>>(result);
        }

        [Fact]
        public async Task GetProject_ShouldCallFindAsync()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var id = "project1";

            // Act
            var result = await _projectService.GetProject(id, cancellationToken);

            // Assert
            _unitOfWorkMock.Verify(x => x.ProjectRepository.FindAsync(
                It.IsAny<Expression<Func<Project, bool>>>(), cancellationToken), Times.Once);
            Assert.IsType<Project>(result);
        }

        [Fact]
        public async Task UpdateProject_ShouldCallUpdateAsyncAndSaveChangesAsync()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var project = new Project() { Id = "project1" };

            // Act
            var result = await _projectService.UpdateProject(project, cancellationToken);

            // Assert
            _unitOfWorkMock.Verify(x => x.ProjectRepository.UpdateAsync(project, project.Id, cancellationToken), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(cancellationToken), Times.Once);
            Assert.Equal(result, project);
        }

        [Fact]
        public async Task GetProjectByName_ShouldCallFindAsync()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var name = "ProjectName";

            // Act
            var result = await _projectService.GetProjectByName(name, cancellationToken);

            // Assert
            _unitOfWorkMock.Verify(x => x.ProjectRepository.FindAsync(
                It.IsAny<Expression<Func<Project, bool>>>(), cancellationToken), Times.Once);
            Assert.IsType<Project>(result);
        }
    }
}

