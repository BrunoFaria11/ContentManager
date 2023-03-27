using System.Threading;
using System.Threading.Tasks;
using Timelogger.Common.Interfaces.Repositories;

namespace Timelogger.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiContext _dbContext;

        public UnitOfWork(ApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        private ProjectRepository _projectRepository;
        public IProjectRepository ProjectRepository => _projectRepository ??= new ProjectRepository(_dbContext);

        private TimerHistoryRepository _timerHistoryRepository;
        public ITimerHistoryRepository TimerHistoryRepository => _timerHistoryRepository ??= new TimerHistoryRepository(_dbContext);


        private InvoiceRepository _invoiceRepository;
        public IInvoiceRepository InvoiceRepository => _invoiceRepository ??= new InvoiceRepository(_dbContext);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            // By implementing a single DbContext with a single call to .SaveChangesAsync()
            // this guarantees by itself that all the work is done in a simple transaction.
            // You only need to use a TransactionScope if there are several .SaveChangesAsync() or even several different DbContext instances involved
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
