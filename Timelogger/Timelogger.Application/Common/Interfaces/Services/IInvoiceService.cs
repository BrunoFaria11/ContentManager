using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Entities;

namespace Timelogger.Common.Interfaces.Services
{
    public interface IInvoiceService
    {
        Task<Invoice> AddInvoice(Invoice invoice, CancellationToken cancellationToken);
        Task<List<Invoice>> GetAllInvoices(bool isCompleted, CancellationToken cancellationToken);
        Task<Invoice> GetInvoice(string id, CancellationToken cancellationToken);
        Task<Invoice> UpdateInvoice(Invoice invoice, CancellationToken cancellationToken);
    }
}

