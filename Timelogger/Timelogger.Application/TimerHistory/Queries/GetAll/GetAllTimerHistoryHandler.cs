using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class GetAllTimerHistoryHandler : IRequestHandler<GetAllTimerHistoryCommand, Response<List<TimerHistory>>>
    {

        private readonly ITimerHistoryService _timerHistoryService;

        public GetAllTimerHistoryHandler(ITimerHistoryService timerHistoryService)
        {
            _timerHistoryService = timerHistoryService;
        }

        public async Task<Response<List<TimerHistory>>> Handle(GetAllTimerHistoryCommand request, CancellationToken cancellationToken)
       {
           var response = await _timerHistoryService.GetAllTimerHistory(request.IsCompleted, cancellationToken);
           return new Response<List<TimerHistory>>(response);
       }
    }
}

