using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timelogger.Commands;

namespace Timelogger.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class InvoiceController : BaseApiController
	{

		public InvoiceController()
		{
		}

		[HttpPost]
		public async Task<IActionResult> CreateInvoice(InvoiceCommand request)
		{
			return Ok(await Mediator.Send(request));
		}

		[HttpGet("GetAllInvoices", Name = "GetAllInvoices")]
		public async Task<IActionResult> GetAllInvoices([FromQuery]GetAllInvoiceCommand request)
		{
			return Ok(await Mediator.Send(request));
		}


		[HttpGet]
		public async Task<IActionResult> GetInvoice([FromQuery] GetInvoiceCommand request)
		{
			return Ok(await Mediator.Send(request));
		}

		[HttpPatch]
		public async Task<IActionResult> UpdateInvoice(UpdateInvoiceCommand request)
		{
			return Ok(await Mediator.Send(request));
		}
	}
}
