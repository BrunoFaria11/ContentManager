using System;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class InvoiceCommand :  IRequest<Response<Invoice>>
    {

        public string ProjectId { get; set; }
        public string DevName { get; set; }
        public string DevDocNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDocNumber { get; set; }
    }
}

