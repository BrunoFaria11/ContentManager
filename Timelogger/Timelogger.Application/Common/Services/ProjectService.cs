using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timelogger.Common.Interfaces.Repositories;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;

namespace Timelogger.Common.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Project> AddProject(Project project, CancellationToken cancellationToken)
        {
            var newProject = await _unitOfWork.ProjectRepository.AddAsync(project, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return newProject;
        }

        public async Task<List<Project>> GetAllProjects(bool isCompleted, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ProjectRepository.FindAllAsync(x=>x.IsCompleted == isCompleted, cancellationToken);
        }

        public async Task<Project> GetProject(string id, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ProjectRepository.FindAsync(x => x.Id == id, cancellationToken);
        }


        public async Task<Project> UpdateProject(Project project, CancellationToken cancellationToken)
        {
            var newProject = await _unitOfWork.ProjectRepository.UpdateAsync(project, project.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return newProject;
        }
    }
}

