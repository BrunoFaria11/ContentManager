
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Entities;

namespace Timelogger.Common.Interfaces.Services
{
    public interface IProjectService
    {
        Task<Project> AddProject(Project project, CancellationToken cancellationToken);
        Task<List<Project>> GetAllProjects(bool isCompleted, CancellationToken cancellationToken);
        Task<Project> GetProject(string id, CancellationToken cancellationToken);
        Task<Project> UpdateProject(Project project, CancellationToken cancellationToken);
    }
}

