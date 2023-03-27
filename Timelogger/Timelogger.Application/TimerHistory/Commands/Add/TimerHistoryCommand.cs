using System;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class TimerHistoryCommand :  IRequest<Response<TimerHistory>>
    {
        public string ProjectId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

