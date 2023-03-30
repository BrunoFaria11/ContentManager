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
    public class TimerHistoryHandlerTests
    {
        private readonly Mock<ITimerHistoryService> _mockTimerHistoryService;
        private readonly Mock<IProjectService> _mockProjectService;
        private readonly TimerHistoryHandler _handler;

        public TimerHistoryHandlerTests()
        {
            _mockTimerHistoryService = new Mock<ITimerHistoryService>();
            _mockProjectService = new Mock<IProjectService>();
            _handler = new TimerHistoryHandler(_mockTimerHistoryService.Object, _mockProjectService.Object);
        }

        [Fact]
        public async Task Handle_ShouldAddTimerHistory_WhenRequestIsValid()
        {
            // Arrange
            var request = new TimerHistoryCommand
            {
                ProjectId = "1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1)
            };
            var project = new Project { Id = "1", TotalTimeSpent = 0 };
            var timerHistory = new TimerHistory { ProjectId = "1", StartDate = request.StartDate, EndDate = request.EndDate, TotalHours = 1 };
            _mockProjectService.Setup(x => x.GetProject(request.ProjectId, It.IsAny<CancellationToken>())).ReturnsAsync(project);
            _mockTimerHistoryService.Setup(x => x.GetAllTimerHistory(request.ProjectId, It.IsAny<CancellationToken>())).ReturnsAsync(new List<TimerHistory>());
            _mockTimerHistoryService.Setup(x => x.AddTimerHistory(It.IsAny<TimerHistory>(),It.IsAny<CancellationToken>())).ReturnsAsync(timerHistory);

            // Act
            var response = await _handler.Handle(request, default);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<Response<TimerHistory>>(response);
            Assert.Equal(timerHistory, response.Data);
            _mockProjectService.Verify(x => x.UpdateProject(project, CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenProjectDoesNotExist()
        {
            // Arrange
            var request = new TimerHistoryCommand
            {
                ProjectId = "1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1)
            };
            _mockProjectService.Setup(x => x.GetProject(request.ProjectId, It.IsAny<CancellationToken>())).ReturnsAsync((Project)null);

            // Act & Assert
            await Assert.ThrowsAsync<ApiException>(() => _handler.Handle(request, CancellationToken.None));
        }

    }
}

