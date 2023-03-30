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
    public class InvoiceHandlerTests
    {
        private readonly Mock<IInvoiceService> _invoiceServiceMock;
        private readonly Mock<IProjectService> _projectServiceMock;
        private readonly InvoiceHandler _handler;

        public InvoiceHandlerTests()
        {
            _invoiceServiceMock = new Mock<IInvoiceService>();
            _projectServiceMock = new Mock<IProjectService>();
            _handler = new InvoiceHandler(_invoiceServiceMock.Object, _projectServiceMock.Object);
        }

        [Fact]
        public async Task Handle_WhenProjectDoesNotExist_ThrowsApiException()
        {
            // Arrange
            var projectId = "project-id";
            var request = new InvoiceCommand { ProjectId = projectId };
            _projectServiceMock
                .Setup(x => x.GetProject(projectId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Project)null);

            // Act & Assert
            await Assert.ThrowsAsync<ApiException>(() => _handler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WhenProjectExists_CallsAddInvoice()
        {
            // Arrange
            var projectId = "project-id";
            var request = new InvoiceCommand { ProjectId = projectId, DevName = "DevName", DevDocNumber= "DevDocNumber", CustomerName = "CustomerName", CustomerDocNumber  = "CustomerDocNumber" };
            var project = new Project { Id = projectId };
            var invoice = new Invoice { ProjectId = projectId };
            _projectServiceMock
                .Setup(x => x.GetProject(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(project);
            _invoiceServiceMock
                .Setup(x => x.AddInvoice(It.IsAny<Invoice>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(invoice);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _invoiceServiceMock.Verify(x => x.AddInvoice(It.IsAny<Invoice>(), It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(invoice, result.Data);
        }
    }

}

