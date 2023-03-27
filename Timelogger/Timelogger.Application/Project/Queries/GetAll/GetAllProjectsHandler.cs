using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsCommand, Response<List<Project>>>
    {

        private readonly IProjectService _projectService;

        public GetAllProjectsHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<Response<List<Project>>> Handle(GetAllProjectsCommand request, CancellationToken cancellationToken)
       {
           var response = await _projectService.GetAllProjects(request.IsCompleted, cancellationToken);
           return new Response<List<Project>>(response);
       }
    }
}

