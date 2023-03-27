using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class GetTimerHistoryHandler : IRequestHandler<GetTimerHistoryCommand, Response<TimerHistory>>
    {

        private readonly ITimerHistoryService _timerHistoryService;

        public GetTimerHistoryHandler(ITimerHistoryService timerHistoryService)
        {
            _timerHistoryService = timerHistoryService;
        }

        public async Task<Response<TimerHistory>> Handle(GetTimerHistoryCommand request, CancellationToken cancellationToken)
       {
           dynamic response = await _timerHistoryService.GetTimerHistory(request.Id, cancellationToken);
           return new Response<TimerHistory>(response);
       }
    }
}

