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
    public class GetInvoiceHandlerTests
    {
        private readonly Mock<IInvoiceService> _invoiceServiceMock = new Mock<IInvoiceService>();
        private readonly GetInvoiceHandler _handler;

        public GetInvoiceHandlerTests()
        {
            _handler = new GetInvoiceHandler(_invoiceServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsResponse()
        {
            // Arrange
            var invoiceId = "1";
            var cancellationToken = new CancellationToken();
            var invoice = new Invoice() { Id = invoiceId };
            _invoiceServiceMock.Setup(svc => svc.GetInvoice(invoiceId, cancellationToken)).ReturnsAsync(invoice);
            var request = new GetInvoiceCommand{ Id = invoiceId };

            // Act
            var result = await _handler.Handle(request, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Response<Invoice>>(result);
            Assert.Equal(invoice, result.Data);
        }
    }

}

