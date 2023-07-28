using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContentManager.Commands;

namespace ContentManager.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ApplicationController : BaseApiController
	{

		public ApplicationController()
		{
		}

		[HttpPost]
		public async Task<IActionResult> CreateApplication(ApplicationCommand request)
		{
			return Ok(await Mediator.Send(request));
		}
	}
}
