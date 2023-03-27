using System;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class UpdateTimerHistoryCommand : IRequest<Response<TimerHistory>>
    {
        public string Id { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

