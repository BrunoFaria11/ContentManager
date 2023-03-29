using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Common.Interfaces.Repositories;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;

namespace Timelogger.Common.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Invoice> AddInvoice(Invoice invoice, CancellationToken cancellationToken)
        {
            var newInvoice = await _unitOfWork.InvoiceRepository.AddAsync(invoice, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return newInvoice;
        }

        public async Task<List<Invoice>> GetAllInvoices(string projectId, CancellationToken cancellationToken)
        {
            return await _unitOfWork.InvoiceRepository.FindAllAsync(x => x.ProjectId == projectId, cancellationToken);
        }

        public async Task<Invoice> GetInvoice(string id, CancellationToken cancellationToken)
        {
            return await _unitOfWork.InvoiceRepository.FindAsync(x => x.Id == id, cancellationToken);
        }


        public async Task<Invoice> UpdateInvoice(Invoice invoice, CancellationToken cancellationToken)
        {
            var newInvoice = await _unitOfWork.InvoiceRepository.UpdateAsync(invoice, invoice.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return newInvoice;
        }
    }
}

