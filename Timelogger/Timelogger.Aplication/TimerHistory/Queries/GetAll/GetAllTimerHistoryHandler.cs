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
    public class GetAllTimerHistoryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsListOfTimerHistory()
        {
            // Arrange
            var timerHistoryServiceMock = new Mock<ITimerHistoryService>();
            var timerHistories = new List<TimerHistory>
        {
            new TimerHistory { Id = "1", ProjectId = "1", StartDate = DateTime.Now },
            new TimerHistory { Id = "2", ProjectId = "1", StartDate = DateTime.Now.AddDays(-1) },
            new TimerHistory { Id = "3", ProjectId = "2", StartDate = DateTime.Now.AddDays(-2) }
        };
            timerHistoryServiceMock.Setup(service => service.GetAllTimerHistory("1", It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(timerHistories);

            var handler = new GetAllTimerHistoryHandler(timerHistoryServiceMock.Object);
            var request = new GetAllTimerHistoryCommand { ProjectId = "1" };

            // Act
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Data);
            Assert.Equal(timerHistories.Count, response.Data.Count);
            timerHistoryServiceMock.Verify(service => service.GetAllTimerHistory("1", It.IsAny<CancellationToken>()), Times.Once);
        }
    }

}

