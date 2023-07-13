using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContentManager.Commands;

namespace ContentManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelController : BaseApiController
    {

        public ModelController()
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateModel(ModelCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateModel(UpdateModelCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("GetAllModels", Name = "GetAllModels")]
        public async Task<IActionResult> GetAllModels([FromQuery] GetAllModelsCommand request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}
