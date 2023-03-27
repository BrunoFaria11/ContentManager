using System;
using System.Collections.Generic;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class GetTimerHistoryCommand :  IRequest<Response<TimerHistory>>
    {
        public string Id { get; set; }
    }
}

