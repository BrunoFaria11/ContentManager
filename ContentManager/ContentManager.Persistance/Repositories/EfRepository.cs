using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContentManager.Common.Interfaces.Repositories;

namespace ContentManager.Repositories
{
   
        public class EfRepository<TObject> : IRepository<TObject> where TObject : class
        {
            protected readonly CMDbContext _dbContext;

            public EfRepository(CMDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<ICollection<TObject>> GetAllAsync(CancellationToken cancellationToken)
            {
                return await _dbContext.Set<TObject>().ToListAsync(cancellationToken);
            }

            public async Task<TObject> GetAsync(long id, CancellationToken cancellationToken)
            {
                return await _dbContext.Set<TObject>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
            }

            public async Task<TObject> FindAsync(Expression<Func<TObject, bool>> match, CancellationToken cancellationToken)
            {
                return await _dbContext.Set<TObject>().SingleOrDefaultAsync(match, cancellationToken);
            }

            public async Task<List<TObject>> FindAllAsync(Expression<Func<TObject, bool>> match, CancellationToken cancellationToken)
            {
                return await _dbContext.Set<TObject>().Where(match).ToListAsync(cancellationToken);
            }

            public async Task<TObject> AddAsync(TObject t, CancellationToken cancellationToken)
            {
                await _dbContext.Set<TObject>().AddAsync(t, cancellationToken);
                return t;
            }

            public async Task<TObject> UpdateAsync(TObject updated, int key, CancellationToken cancellationToken)
            {
                if (updated == null) return null;

                TObject existing = await _dbContext.Set<TObject>().FindAsync(new object[] { key }, cancellationToken: cancellationToken);

                if (existing != null) _dbContext.Entry(existing).CurrentValues.SetValues(updated);

                return existing;
            }

            public void UpdateRange(IEnumerable<TObject> updated)
            {
                _dbContext.Set<TObject>().UpdateRange(updated);
            }

            public void RemoveAsync(TObject t)
            {
                _dbContext.Set<TObject>().Remove(t);
            }

            public async Task<int> CountAsync(CancellationToken cancellationToken)
            {
                return await _dbContext.Set<TObject>().CountAsync(cancellationToken);
            }

            public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
            {
                return await _dbContext.SaveChangesAsync(cancellationToken);
            }
    }
}

