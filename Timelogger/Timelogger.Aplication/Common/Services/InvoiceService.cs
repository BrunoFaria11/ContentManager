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
    public class InvoiceServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly InvoiceService _invoiceService;

        public InvoiceServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _invoiceService = new InvoiceService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task AddInvoice_ShouldCallAddAsyncAndSaveChangesAsync()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var invoice = new Invoice();

            // Act
            _unitOfWorkMock.Setup(x => x.InvoiceRepository.AddAsync(It.IsAny<Invoice>(), It.IsAny<CancellationToken>())).ReturnsAsync(invoice);
            var result = await _invoiceService.AddInvoice(invoice, cancellationToken);

            // Assert
            _unitOfWorkMock.Verify(x => x.InvoiceRepository.AddAsync(invoice, cancellationToken), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(cancellationToken), Times.Once);
            Assert.Equal(result, invoice);
        }

        [Fact]
        public async Task GetAllInvoices_ShouldCallFindAllAsync()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var projectId = "project1";
            var invoices = new List<Invoice>() {
                new Invoice() { Id = "invoice1" }
            };

            // Act
            _unitOfWorkMock.Setup(x => x.InvoiceRepository.FindAllAsync(It.IsAny<Expression<Func<Invoice, bool>>>(), cancellationToken)).ReturnsAsync(invoices);
            var result = await _invoiceService.GetAllInvoices(projectId, cancellationToken);

            // Assert
            _unitOfWorkMock.Verify(x => x.InvoiceRepository.FindAllAsync(
                It.IsAny<Expression<Func<Invoice, bool>>>(), cancellationToken), Times.Once);
            Assert.IsType<List<Invoice>>(result);
        }

        [Fact]
        public async Task GetInvoice_ShouldCallFindAsync()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var id = "invoice1";
            var invoice = new Invoice() { Id = "invoice1" };

            // Act
            _unitOfWorkMock.Setup(x => x.InvoiceRepository.FindAsync(It.IsAny<Expression<Func<Invoice, bool>>>(), cancellationToken)).ReturnsAsync(invoice);
            var result = await _invoiceService.GetInvoice(id, cancellationToken);

            // Assert
            _unitOfWorkMock.Verify(x => x.InvoiceRepository.FindAsync(
                It.IsAny<Expression<Func<Invoice, bool>>>(), cancellationToken), Times.Once);
            Assert.IsType<Invoice>(result);
        }

        [Fact]
        public async Task UpdateInvoice_ShouldCallUpdateAsyncAndSaveChangesAsync()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var invoice = new Invoice() { Id = "invoice1" };

            // Act
            _unitOfWorkMock.Setup(x => x.InvoiceRepository.UpdateAsync(It.IsAny<Invoice>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(invoice);
            var result = await _invoiceService.UpdateInvoice(invoice, cancellationToken);

            // Assert
            _unitOfWorkMock.Verify(x => x.InvoiceRepository.UpdateAsync(invoice, invoice.Id, cancellationToken), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(cancellationToken), Times.Once);
            Assert.Equal(result, invoice);
        }
    }

}

