using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class UpdateProjectsHandler : IRequestHandler<UpdateProjectsCommand, Response<Project>>
    {

        private readonly IProjectService _projectService;

        public UpdateProjectsHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<Response<Project>> Handle(UpdateProjectsCommand request, CancellationToken cancellationToken)
       {
            var project = await _projectService.GetProject(request.Id, cancellationToken);

            project.Name = request.Name;
            project.DeadLine = request.DeadLine;
            project.TimePerWeek = request.TimePerWeek;
            project.IsCompleted = request.IsCompleted;


           var response = await _projectService.UpdateProject(project, cancellationToken);
           return new Response<Project>(response);
       }
    }
}

