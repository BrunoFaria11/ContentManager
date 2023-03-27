using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class GetInvoiceHandler : IRequestHandler<GetInvoiceCommand, Response<Invoice>>
    {

        private readonly IInvoiceService _invoiceService;

        public GetInvoiceHandler(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public async Task<Response<Invoice>> Handle(GetInvoiceCommand request, CancellationToken cancellationToken)
       {
           dynamic response = await _invoiceService.GetInvoice(request.Id, cancellationToken);
           return new Response<Invoice>(response);
       }
    }
}

