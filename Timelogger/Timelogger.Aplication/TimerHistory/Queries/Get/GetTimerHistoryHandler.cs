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
    public class GetTimerHistoryHandlerTests
    {
        [Fact]
        public async Task GetTimerHistoryHandler_ReturnsTimerHistory()
        {
            // Arrange
            var timerHistoryId = "1";
            var expectedTimerHistory = new TimerHistory { Id = timerHistoryId };
            var mockTimerHistoryService = new Mock<ITimerHistoryService>();
            mockTimerHistoryService
                .Setup(x => x.GetTimerHistory(timerHistoryId, default))
                .ReturnsAsync(expectedTimerHistory);
            var handler = new GetTimerHistoryHandler(mockTimerHistoryService.Object);

            // Act
            var result = await handler.Handle(new GetTimerHistoryCommand { Id = timerHistoryId }, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedTimerHistory.Id, result.Data.Id);
        }
    }

}

