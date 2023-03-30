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
            _unitOfWorkMock.Setup(x => x.ProjectRepository.AddAsync(It.IsAny<Project>(), It.IsAny<CancellationToken>())).ReturnsAsync(project);
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
            var projects = new List<Project>() {
                new Project() { Id = "project1", Name = "ProjectName" }
            };

            // Act
            _unitOfWorkMock.Setup(x => x.ProjectRepository.FindAllAsync(It.IsAny<Expression<Func<Project, bool>>>(), cancellationToken)).ReturnsAsync(projects);
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
            var project = new Project() { Id = "project1", Name = "ProjectName" };

            // Act
            _unitOfWorkMock.Setup(x => x.ProjectRepository.FindAsync(It.IsAny<Expression<Func<Project, bool>>>(), cancellationToken)).ReturnsAsync(project);
            var result = await _projectService.GetProject(project.Id, cancellationToken);

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
            _unitOfWorkMock.Setup(x => x.ProjectRepository.UpdateAsync(It.IsAny<Project>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(project);
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
            var project = new Project() { Id = "project1", Name = "ProjectName" };

            // Act
            _unitOfWorkMock.Setup(x => x.ProjectRepository.FindAsync(It.IsAny<Expression<Func<Project, bool>>>(), cancellationToken)).ReturnsAsync(project);
            var result = await _projectService.GetProjectByName(project.Name, cancellationToken);

            // Assert
            _unitOfWorkMock.Verify(x => x.ProjectRepository.FindAsync(
                It.IsAny<Expression<Func<Project, bool>>>(), cancellationToken), Times.Once);
            Assert.IsType<Project>(result);
        }
    }
}

