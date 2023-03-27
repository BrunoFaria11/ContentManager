using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Application.Common.Exceptions;
using Timelogger.Application.Common.Models;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class UpdateTimerHistoryHandler : IRequestHandler<UpdateTimerHistoryCommand, Response<TimerHistory>>
    {

        private readonly ITimerHistoryService _timerHistoryService;
        private readonly IProjectService _projectService;

        public UpdateTimerHistoryHandler(ITimerHistoryService timerHistoryService, IProjectService projectService)
        {
            _timerHistoryService = timerHistoryService;
            _projectService = projectService;
        }

        public async Task<Response<TimerHistory>> Handle(UpdateTimerHistoryCommand request, CancellationToken cancellationToken)
        {
            var timerHistory = await _timerHistoryService.GetTimerHistory(request.Id, cancellationToken);

            if (timerHistory == null)
            {
                throw new ApiException($"Timer History doesn't exist");
            }

            var project = await _projectService.GetProject(timerHistory.ProjectId, cancellationToken);

            if (project == null)
            {
                throw new ApiException($"Project doesn't exist");
            }

            timerHistory.EndDate = request.EndDate;
            TimeSpan ts = timerHistory.EndDate.Value - timerHistory.StartDate;
            timerHistory.TotalHours = ts.TotalHours;

            project.TotalTimeSpent += timerHistory.TotalHours ?? 0;
            _ = await _projectService.UpdateProject(project, cancellationToken);

            var response = await _timerHistoryService.UpdateTimerHistory(timerHistory, cancellationToken);
            return new Response<TimerHistory>(response);
        }
    }
}

