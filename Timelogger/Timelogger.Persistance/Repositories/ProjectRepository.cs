using System;
using System.Threading.Tasks;
using Timelogger.Common.Interfaces.Repositories;
using Timelogger.Entities;

namespace Timelogger.Repositories
{
    public class ProjectRepository : EfRepository<Project>, IProjectRepository
    {
        private readonly ApiContext _context;

        public ProjectRepository(ApiContext context) : base(context)
        {
            _context = context;
        }

       
    }
}

