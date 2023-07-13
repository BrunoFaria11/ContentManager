using System.Threading;
using System.Threading.Tasks;
using ContentManager.Common.Interfaces.Repositories;

namespace ContentManager.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CMDbContext _dbContext;

        public UnitOfWork(CMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private ApplicationRepository _applicationRepository;
        public IApplicationRepository ApplicationRepository => _applicationRepository ??= new ApplicationRepository(_dbContext);

        private ModelRepository _modelRepository;
        public IModelRepository ModelRepository => _modelRepository ??= new ModelRepository(_dbContext);


        private UserRepository _userRepository;
        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_dbContext);

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
