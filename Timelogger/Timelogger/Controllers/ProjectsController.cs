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
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("GetAllProjects", Name = "GetAllProjects")]
        public async Task<IActionResult> GetAllProjects([FromQuery] GetAllProjectsCommand request)
        {
            return Ok(await Mediator.Send(request));
        }


        [HttpGet]
        public async Task<IActionResult> GetProject([FromQuery] GetProjectsCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateProject(UpdateProjectsCommand request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}
