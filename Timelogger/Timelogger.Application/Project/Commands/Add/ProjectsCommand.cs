using System;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class ProjectsCommand :  IRequest<Response<Project>>
    {
        public string Name { get; set; }
        public DateTime DeadLine { get; set; }
        public decimal TimePerWeek { get; set; }
    }
}

