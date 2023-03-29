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
			return Ok(await Mediator.Send(request));
		}

		[HttpGet("GetAllTimerHistories", Name = "GetAllTimerHistories")]
		public async Task<IActionResult> GetAllTimerHistories([FromQuery]GetAllTimerHistoryCommand request)
		{
			return Ok(await Mediator.Send(request));
		}


		[HttpGet]
		public async Task<IActionResult> GetTimerHistory([FromQuery] GetTimerHistoryCommand request)
		{
			return Ok(await Mediator.Send(request));
		}

		[HttpPatch]
		public async Task<IActionResult> UpdateTimerHistory(UpdateTimerHistoryCommand request)
		{
			return Ok(await Mediator.Send(request));
		}
	}
}
