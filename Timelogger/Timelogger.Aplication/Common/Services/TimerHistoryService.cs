using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Timelogger.Common.Interfaces.Repositories;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Common.Services;
using Timelogger.Entities;
using Xunit;

namespace Timelogger.Aplication.Test
{
    public class TimerHistoryServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ITimerHistoryService _timerHistoryService;

        public TimerHistoryServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _timerHistoryService = new TimerHistoryService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task AddTimerHistory_ShouldReturnNewTimerHistory()
        {
            // Arrange
            var timerHistory = new TimerHistory
            {
                ProjectId = "123",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddHours(1)
            };
            var cancellationToken = new CancellationToken();

            _unitOfWorkMock.Setup(x => x.TimerHistoryRepository.AddAsync(timerHistory, cancellationToken))
                .ReturnsAsync(timerHistory);

            // Act
            var result = await _timerHistoryService.AddTimerHistory(timerHistory, cancellationToken);

            // Assert
            Assert.Equal(timerHistory, result);
        }

        [Fact]
        public async Task GetAllTimerHistory_ShouldReturnListOfTimerHistory()
        {
            // Arrange
            var projectId = "123";
            var timerHistoryList = new List<TimerHistory>
        {
            new TimerHistory { Id = "1", ProjectId = projectId },
            new TimerHistory { Id = "2", ProjectId = projectId },
            new TimerHistory { Id = "3", ProjectId = projectId }
        };
            var cancellationToken = new CancellationToken();

            _unitOfWorkMock.Setup(x => x.TimerHistoryRepository.FindAllAsync(
                    It.IsAny<Expression<Func<TimerHistory, bool>>>(), cancellationToken))
                .ReturnsAsync(timerHistoryList);

            // Act
            var result = await _timerHistoryService.GetAllTimerHistory(projectId, cancellationToken);

            // Assert
            Assert.Equal(timerHistoryList, result);
        }

        [Fact]
        public async Task GetTimerHistory_ShouldReturnTimerHistory()
        {
            // Arrange
            var id = "1";
            var timerHistory = new TimerHistory { Id = id };
            var cancellationToken = new CancellationToken();

            _unitOfWorkMock.Setup(x => x.TimerHistoryRepository.FindAsync(
                    It.IsAny<Expression<Func<TimerHistory, bool>>>(), cancellationToken))
                .ReturnsAsync(timerHistory);

            // Act
            var result = await _timerHistoryService.GetTimerHistory(id, cancellationToken);

            // Assert
            Assert.Equal(timerHistory, result);
        }

        [Fact]
        public async Task UpdateTimerHistory_ShouldReturnNewTimerHistory()
        {
            // Arrange
            var timerHistory = new TimerHistory
            {
                Id = "1",
                ProjectId = "123",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddHours(1)
            };
            var cancellationToken = new CancellationToken();

            _unitOfWorkMock.Setup(x => x.TimerHistoryRepository.UpdateAsync(timerHistory, timerHistory.Id, cancellationToken))
                .ReturnsAsync(timerHistory);

            // Act
            var result = await _timerHistoryService.UpdateTimerHistory(timerHistory, cancellationToken);

            // Assert
            Assert.Equal(timerHistory, result);
        }
    }
}

