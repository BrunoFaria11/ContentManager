using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timelogger.Commands;

namespace Timelogger.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProjectsController : BaseApiController
	{

		public ProjectsController()
		{
		}

		[HttpPost]
		public async Task<IActionResult> CreateProject(ProjectsCommand request)
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
		
		[HttpGet("GetAllProjects", Name = "GetAllProjects")]
		public async Task<IActionResult> GetAllProjects([FromQuery]GetAllProjectsCommand request)
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
		public async Task<IActionResult> GetProject([FromQuery] GetProjectsCommand request)
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
		public async Task<IActionResult> UpdateProject(UpdateProjectsCommand request)
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
