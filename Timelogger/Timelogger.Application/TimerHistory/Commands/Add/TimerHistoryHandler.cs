using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;
using Timelogger.Commands;
using Timelogger.Application.Common.Exceptions;

namespace Timelogger.Commands
{
    public class TimerHistoryHandler : IRequestHandler<TimerHistoryCommand, Response<TimerHistory>>
    {

        private readonly ITimerHistoryService _timerHistoryService;
        private readonly IProjectService _projectService;

        public TimerHistoryHandler(ITimerHistoryService timerHistoryService, IProjectService projectService)
        {
            _timerHistoryService = timerHistoryService;
            _projectService = projectService;
        }

        public async Task<Response<TimerHistory>> Handle(TimerHistoryCommand request, CancellationToken cancellationToken)
        {

            if (await _projectService.GetProject(request.ProjectId, cancellationToken) == null)
            {
                throw new ApiException($"Project doesn't exist");
            }

            TimerHistory timerHistory = new TimerHistory()
            {
                ProjectId = request.ProjectId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };

            var response = await _timerHistoryService.AddTimerHistory(timerHistory, cancellationToken);
            return new Response<TimerHistory>(response);
        }
    }
}

