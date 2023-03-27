using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timelogger.Commands;

namespace Timelogger.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TimerHistoryController : BaseApiController
	{

		public TimerHistoryController()
		{
		}

		[HttpPost]
		public async Task<IActionResult> CreateTimerHistory(TimerHistoryCommand request)
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

		[HttpGet("GetAllTimerHistories", Name = "GetAllTimerHistories")]
		public async Task<IActionResult> GetAllTimerHistories([FromQuery]GetAllTimerHistoryCommand request)
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
		public async Task<IActionResult> GetTimerHistory([FromQuery] GetTimerHistoryCommand request)
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
		public async Task<IActionResult> UpdateTimerHistory(UpdateTimerHistoryCommand request)
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
