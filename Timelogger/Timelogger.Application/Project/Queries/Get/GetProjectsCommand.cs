using System;
using System.Collections.Generic;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class GetProjectsCommand :  IRequest<Response<Project>>
    {
        public string Id { get; set; }
    }
}

