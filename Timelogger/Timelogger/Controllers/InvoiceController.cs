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
			try
			{
				var response = await Mediator.Send(request);
				return response is not null ? Ok(response) : NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("GetAllInvoices", Name = "GetAllInvoices")]
		public async Task<IActionResult> GetAllInvoices([FromQuery]GetAllInvoiceCommand request)
		{
			try
			{
				var response = await Mediator.Send(request);
				return response is not null ? Ok(response) : NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		[HttpGet]
		public async Task<IActionResult> GetInvoice([FromQuery] GetInvoiceCommand request)
		{
			try
			{
				var response = await Mediator.Send(request);
				return response is not null ? Ok(response) : NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPatch]
		public async Task<IActionResult> UpdateInvoice(UpdateInvoiceCommand request)
		{
			try
			{
				var response = await Mediator.Send(request);
				return response is not null ? Ok(response) : NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
