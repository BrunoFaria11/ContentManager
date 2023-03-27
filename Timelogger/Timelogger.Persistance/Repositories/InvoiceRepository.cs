using Timelogger.Common.Interfaces.Repositories;
using Timelogger.Entities;

namespace Timelogger.Repositories
{
    public class InvoiceRepository : EfRepository<Invoice>, IInvoiceRepository
    {
        private readonly ApiContext _context;

        public InvoiceRepository(ApiContext context) : base(context)
        {
            _context = context;
        }

    }
}

