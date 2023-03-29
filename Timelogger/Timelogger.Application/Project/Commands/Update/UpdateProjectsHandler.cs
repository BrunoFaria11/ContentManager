using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Application.Common.Exceptions;
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
            var project = await _projectService.GetProjectByName(request.Name, cancellationToken);
            if (project != null && project.Id != request.Id)
            {
                throw new ApiException($"Project already exist");
            }

            var updatedProject = await _projectService.GetProject(request.Id, cancellationToken);

            updatedProject.Name = request.Name;
            updatedProject.DeadLine = request.DeadLine;
            updatedProject.TimePerWeek = request.TimePerWeek;
            updatedProject.IsCompleted = request.IsCompleted;


           var response = await _projectService.UpdateProject(updatedProject, cancellationToken);
           return new Response<Project>(response);
       }
    }
}

