using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManager.Common.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IApplicationRepository ApplicationRepository { get; }
        IUserRepository UserRepository { get; }
        IModelRepository ModelRepository { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
