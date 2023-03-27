using System;
using System.Threading;
using System.Threading.Tasks;

namespace Timelogger.Common.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProjectRepository ProjectRepository { get; }
        ITimerHistoryRepository TimerHistoryRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
