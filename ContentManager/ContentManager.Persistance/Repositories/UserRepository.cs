using System;
using ContentManager.Common.Interfaces.Repositories;
using ContentManager.Domain.Entities;

namespace ContentManager.Repositories
{
    public class UserRepository : EfRepository<Users>, IUserRepository
    {
        private readonly CMDbContext _context;

        public UserRepository(CMDbContext context) : base(context)
        {
            _context = context;
        }

    }
}

