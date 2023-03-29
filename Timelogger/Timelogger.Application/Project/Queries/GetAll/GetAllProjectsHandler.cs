using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;
using System.Linq;

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
            List<string> list = new List<string>();
            var response = await _projectService.GetAllProjects(request.IsCompleted, cancellationToken);

            if (request.IsToSortDesc)
            {
                response = response.OrderByDescending(x => x.DeadLine).ToList();
            }
            else
            {
                response = response.OrderBy(x => x.DeadLine).ToList();
            }

            return new Response<List<Project>>(response);
        }
    }
}

