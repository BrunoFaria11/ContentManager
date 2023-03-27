using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Timelogger.Application.Common.Exceptions;
using Timelogger.Application.Common.Models;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Entities;

namespace Timelogger.Commands
{
    public class UpdateInvoiceHandler : IRequestHandler<UpdateInvoiceCommand, Response<Invoice>>
    {

        private readonly IInvoiceService _invoiceService;
        private readonly IProjectService _projectService;

        public UpdateInvoiceHandler(IInvoiceService invoiceService, IProjectService projectService)
        {
            _invoiceService = invoiceService;
            _projectService = projectService;
        }

        public async Task<Response<Invoice>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceService.GetInvoice(request.Id, cancellationToken);

            if (invoice == null)
            {
                throw new ApiException($"Timer History doesn't exist");
            }

            var project = await _projectService.GetProject(invoice.ProjectId, cancellationToken);

            if (project == null)
            {
                throw new ApiException($"Project doesn't exist");
            }

            invoice.DevName = request.DevName;
            invoice.DevDocNumber = request.DevDocNumber;
            invoice.CustomerName = request.CustomerName;
            invoice.CustomerDocNumber = request.CustomerDocNumber;

            _ = await _projectService.UpdateProject(project, cancellationToken);

            var response = await _invoiceService.UpdateInvoice(invoice, cancellationToken);
            return new Response<Invoice>(response);
        }
    }
}

