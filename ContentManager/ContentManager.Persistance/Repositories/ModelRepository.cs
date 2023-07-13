using System;
using System.Threading.Tasks;
using ContentManager.Common.Interfaces.Repositories;
using ContentManager.Domain.Entities;

namespace ContentManager.Repositories
{
    public class ModelRepository : EfRepository<Models>, IModelRepository
    {
        private readonly CMDbContext _context;

        public ModelRepository(CMDbContext context) : base(context)
        {
            _context = context;
        }

       
    }
}

