using ContentManager.Common.Interfaces.Repositories;

namespace ContentManager.Repositories
{
    public class ApplicationRepository : EfRepository<ContentManager.Domain.Entities.Application>, IApplicationRepository
    {
        private readonly CMDbContext _context;

        public ApplicationRepository(CMDbContext context) : base(context)
        {
            _context = context;
        }

    }
}

