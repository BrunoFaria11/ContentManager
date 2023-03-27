using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Application.Common.Models;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;
using Timelogger.Commands;
using Timelogger.Application.Common.Exceptions;

namespace Timelogger.Commands
{
    public class InvoiceHandler : IRequestHandler<InvoiceCommand, Response<Invoice>>
    {

        private readonly IInvoiceService _invoiceService;
        private readonly IProjectService _projectService;

        public InvoiceHandler(IInvoiceService invoiceService, IProjectService projectService)
        {
            _invoiceService = invoiceService;
            _projectService = projectService;
        }

        public async Task<Response<Invoice>> Handle(InvoiceCommand request, CancellationToken cancellationToken)
        {

            if (await _projectService.GetProject(request.ProjectId, cancellationToken) == null)
            {
                throw new ApiException($"Project doesn't exist");
            }

            Invoice Invoice = new Invoice()
            {
                ProjectId = request.ProjectId,
                DevName = request.DevName,
                DevDocNumber = request.DevDocNumber,
                CustomerName = request.CustomerName,
                CustomerDocNumber = request.CustomerDocNumber,
                InvoiceDate = System.DateTime.Now,
            };

            var response = await _invoiceService.AddInvoice(Invoice, cancellationToken);
            return new Response<Invoice>(response);
        }
    }
}

