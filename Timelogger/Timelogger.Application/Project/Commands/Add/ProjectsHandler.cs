using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class ProjectsHandler : IRequestHandler<ProjectsCommand, Response<Project>>
    {

        private readonly IProjectService _projectService;

        public ProjectsHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<Response<Project>> Handle(ProjectsCommand request, CancellationToken cancellationToken)
       {
            Project project = new Project()
            {
                Name = request.Name,
                DeadLine = request.DeadLine,
                TimePerWeek = request.TimePerWeek,
            };

           var response = await _projectService.AddProject(project, cancellationToken);
           return new Response<Project>(response);
       }
    }
}

