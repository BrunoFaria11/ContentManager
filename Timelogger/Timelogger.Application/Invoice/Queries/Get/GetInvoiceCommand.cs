using System;
using System.Collections.Generic;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class GetInvoiceCommand :  IRequest<Response<Invoice>>
    {
        public string Id { get; set; }
    }
}

