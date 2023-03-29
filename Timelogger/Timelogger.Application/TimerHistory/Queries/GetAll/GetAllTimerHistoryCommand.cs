using System;
using System.Collections.Generic;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class GetAllTimerHistoryCommand :  IRequest<Response<List<TimerHistory>>>
    {
        public string ProjectId { get; set; }
    }
}

