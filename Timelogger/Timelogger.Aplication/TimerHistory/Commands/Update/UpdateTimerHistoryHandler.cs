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
    public class UpdateTimerHistoryHandlerTests
    {
        private readonly Mock<ITimerHistoryService> _timerHistoryServiceMock;
        private readonly Mock<IProjectService> _projectServiceMock;
        private readonly UpdateTimerHistoryHandler _handler;

        public UpdateTimerHistoryHandlerTests()
        {
            _timerHistoryServiceMock = new Mock<ITimerHistoryService>();
            _projectServiceMock = new Mock<IProjectService>();
            _handler = new UpdateTimerHistoryHandler(_timerHistoryServiceMock.Object, _projectServiceMock.Object);
        }

        [Fact]
        public async Task Handle_TimerHistoryExistsAndProjectExists_ReturnsResponse()
        {
            // Arrange
            var timerHistory = new TimerHistory { Id = "1", ProjectId = "1", StartDate = DateTime.Now.AddDays(-1), EndDate = null };
            var project = new Project { Id = "1", TotalTimeSpent = 5 };
            var request = new UpdateTimerHistoryCommand { Id = "1", EndDate = DateTime.Now };
            _timerHistoryServiceMock.Setup(x => x.GetTimerHistory(request.Id, CancellationToken.None)).ReturnsAsync(timerHistory);
            _projectServiceMock.Setup(x => x.GetProject(timerHistory.ProjectId, CancellationToken.None)).ReturnsAsync(project);
            _projectServiceMock.Setup(x => x.UpdateProject(project, CancellationToken.None));
            _timerHistoryServiceMock.Setup(x => x.UpdateTimerHistory(timerHistory, CancellationToken.None)).ReturnsAsync(timerHistory);

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(timerHistory.Id, response.Data.Id);
        }

        [Fact]
        public async Task Handle_TimerHistoryDoesNotExist_ThrowsApiException()
        {
            // Arrange
            var request = new UpdateTimerHistoryCommand { Id = "1", EndDate = DateTime.Now };
            _timerHistoryServiceMock.Setup(x => x.GetTimerHistory(request.Id, CancellationToken.None)).ReturnsAsync((TimerHistory)null);

            // Act & Assert
            await Assert.ThrowsAsync<ApiException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ProjectDoesNotExist_ThrowsApiException()
        {
            // Arrange
            var timerHistory = new TimerHistory { Id = "1", ProjectId = "1", StartDate = DateTime.Now.AddDays(-1), EndDate = null };
            var request = new UpdateTimerHistoryCommand { Id = "1", EndDate = DateTime.Now };
            _timerHistoryServiceMock.Setup(x => x.GetTimerHistory(request.Id, CancellationToken.None)).ReturnsAsync(timerHistory);
            _projectServiceMock.Setup(x => x.GetProject(timerHistory.ProjectId, CancellationToken.None)).ReturnsAsync((Project)null);

            // Act & Assert
            await Assert.ThrowsAsync<ApiException>(() => _handler.Handle(request, CancellationToken.None));
        }
    }

}

