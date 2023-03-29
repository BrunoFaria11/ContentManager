using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Timelogger.Application.Common.Models;
using Timelogger.Commands;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;
using Xunit;

namespace Timelogger.Aplication.Test
{
    public class GetAllInvoiceHandlerTests
    {
        private readonly Mock<IInvoiceService> _mockInvoiceService = new Mock<IInvoiceService>();
        private readonly GetAllInvoiceHandler _handler;

        public GetAllInvoiceHandlerTests()
        {
            _handler = new GetAllInvoiceHandler(_mockInvoiceService.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsListOfInvoices()
        {
            // Arrange
            var projectId = "123";
            var invoices = new List<Invoice> { new Invoice { Id = "1" }, new Invoice { Id = "2" } };
            _mockInvoiceService.Setup(s => s.GetAllInvoices(projectId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(invoices);

            var request = new GetAllInvoiceCommand { ProjectId = projectId };

            // Act
            var response = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsType<Response<List<Invoice>>>(response);
            Assert.Equal(invoices, response.Data);
        }
    }

}

