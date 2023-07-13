using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContentManager.Commands;

namespace ContentManager.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EmailController : BaseApiController
	{

		public EmailController()
		{
		}

		[HttpPost]
		public async Task<IActionResult> SendEmail(EmailCommand request)
		{
			return Ok(await Mediator.Send(request));
		}
	}
}
