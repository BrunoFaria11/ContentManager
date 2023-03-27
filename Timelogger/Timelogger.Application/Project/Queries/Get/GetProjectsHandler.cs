using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class GetProjectsHandler : IRequestHandler<GetProjectsCommand, Response<Project>>
    {

        private readonly IProjectService _projectService;

        public GetProjectsHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<Response<Project>> Handle(GetProjectsCommand request, CancellationToken cancellationToken)
       {
           dynamic response = await _projectService.GetProject(request.Id, cancellationToken);
           return new Response<Project>(response);
       }
    }
}

