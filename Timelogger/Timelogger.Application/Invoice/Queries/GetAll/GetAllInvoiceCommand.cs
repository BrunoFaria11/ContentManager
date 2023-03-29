using System;
using System.Collections.Generic;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class GetAllInvoiceCommand :  IRequest<Response<List<Invoice>>>
    {
        public string ProjectId { get; set; }
    }
}

