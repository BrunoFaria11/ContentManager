using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Timelogger.Common.Interfaces.Repositories
{
    public interface IRepository<TObject> where TObject : class
    {
        Task<ICollection<TObject>> GetAllAsync(CancellationToken cancellationToken);
        Task<TObject> GetAsync(long id, CancellationToken cancellationToken);
        Task<TObject> FindAsync(Expression<Func<TObject, bool>> match, CancellationToken cancellationToken);
        Task<List<TObject>> FindAllAsync(Expression<Func<TObject, bool>> match, CancellationToken cancellationToken);
        Task<TObject> AddAsync(TObject t, CancellationToken cancellationToken);
        Task<TObject> UpdateAsync(TObject updated, string key, CancellationToken cancellationToken);
        void UpdateRange(IEnumerable<TObject> updated);
        void RemoveAsync(TObject t);
        Task<int> CountAsync(CancellationToken cancellationToken);
    }
}

