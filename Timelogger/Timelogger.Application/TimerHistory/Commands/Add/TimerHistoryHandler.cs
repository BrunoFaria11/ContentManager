using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;
using Timelogger.Commands;
using Timelogger.Application.Common.Exceptions;
using System;
using System.Linq;

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
            var project = await _projectService.GetProject(request.ProjectId, cancellationToken);
            var histories = await _timerHistoryService.GetAllTimerHistory(request.ProjectId, cancellationToken);

            if (project == null)
            {
                throw new ApiException($"Project doesn't exist");
            }

            if (histories.Any(x=> (x.StartDate.Date >= request.StartDate && x.StartDate <= request.EndDate)))
            {
                throw new ApiException($"History already exist");
            }

            TimerHistory timerHistory = new TimerHistory()
            {
                ProjectId = request.ProjectId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };

            if (request.EndDate != null)
            {
                TimeSpan ts = timerHistory.EndDate.Value - timerHistory.StartDate;
                timerHistory.TotalHours = ts.TotalHours;

                project.TotalTimeSpent += timerHistory.TotalHours ?? 0;
                _ = await _projectService.UpdateProject(project, cancellationToken);
            }

            var response = await _timerHistoryService.AddTimerHistory(timerHistory, cancellationToken);
            return new Response<TimerHistory>(response);
        }
    }
}

