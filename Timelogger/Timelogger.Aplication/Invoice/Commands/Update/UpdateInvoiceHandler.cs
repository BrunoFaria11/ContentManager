using System.Threading;
using System.Threading.Tasks;
using Moq;
using Timelogger.Application.Common.Exceptions;
using Timelogger.Commands;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;
using Xunit;

namespace Timelogger.Aplication.Test
{
    public class UpdateInvoiceHandlerTests
    {
        private readonly Mock<IInvoiceService> _mockInvoiceService;
        private readonly Mock<IProjectService> _mockProjectService;
        private readonly UpdateInvoiceHandler _handler;

        public UpdateInvoiceHandlerTests()
        {
            _mockInvoiceService = new Mock<IInvoiceService>();
            _mockProjectService = new Mock<IProjectService>();
            _handler = new UpdateInvoiceHandler(_mockInvoiceService.Object, _mockProjectService.Object);
        }

        [Fact]
        public async Task Handle_ThrowsApiException_WhenInvoiceNotFound()
        {
            // Arrange
            var request = new UpdateInvoiceCommand { Id = "1" };
            _mockInvoiceService.Setup(x => x.GetInvoice(request.Id, It.IsAny<CancellationToken>())).ReturnsAsync((Invoice)null);

            // Act & Assert
            await Assert.ThrowsAsync<ApiException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ThrowsApiException_WhenProjectNotFound()
        {
            // Arrange
            var request = new UpdateInvoiceCommand { Id = "1" };
            var invoice = new Invoice { ProjectId = "1" };
            _mockInvoiceService.Setup(x => x.GetInvoice(request.Id, It.IsAny<CancellationToken>())).ReturnsAsync(invoice);
            _mockProjectService.Setup(x => x.GetProject(invoice.ProjectId, It.IsAny<CancellationToken>())).ReturnsAsync((Project)null);

            // Act & Assert
            await Assert.ThrowsAsync<ApiException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ReturnsUpdatedInvoice_WhenInvoiceAndProjectExist()
        {
            // Arrange
            var request = new UpdateInvoiceCommand { Id = "1", DevName = "New Dev Name" };
            var invoice = new Invoice { Id = "1", ProjectId = "1", DevName = "Old Dev Name" };
            var project = new Project { Id = "1" };
            var updatedInvoice = new Invoice { Id = "1", ProjectId = "1", DevName = "New Dev Name" };
            _mockInvoiceService.Setup(x => x.GetInvoice(request.Id, It.IsAny<CancellationToken>())).ReturnsAsync(invoice);
            _mockProjectService.Setup(x => x.GetProject(invoice.ProjectId, It.IsAny<CancellationToken>())).ReturnsAsync(project);
            _mockInvoiceService.Setup(x => x.UpdateInvoice(invoice, It.IsAny<CancellationToken>())).ReturnsAsync(updatedInvoice);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal(request.DevName, result.Data.DevName);
        }
    }

}

