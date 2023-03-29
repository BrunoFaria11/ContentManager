using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class GetAllInvoiceHandler : IRequestHandler<GetAllInvoiceCommand, Response<List<Invoice>>>
    {

        private readonly IInvoiceService _invoiceService;

        public GetAllInvoiceHandler(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public async Task<Response<List<Invoice>>> Handle(GetAllInvoiceCommand request, CancellationToken cancellationToken)
       {
           var response = await _invoiceService.GetAllInvoices(request.ProjectId, cancellationToken);
           return new Response<List<Invoice>>(response);
       }
    }
}

