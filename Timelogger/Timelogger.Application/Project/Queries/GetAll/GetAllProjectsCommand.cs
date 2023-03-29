using System;
using System.Collections.Generic;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class GetAllProjectsCommand :  IRequest<Response<List<Project>>>
    {
        public bool IsCompleted { get; set; }
        public bool IsToSortDesc { get; set; }
    }
}

