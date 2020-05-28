using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers 
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly DemoService _demoService;

        public DemoController(DemoService demoService) 
        {
            _demoService = demoService;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<Demo>>> Get() =>            
            await _demoService.Get();

        [HttpGet("{id:length(24)}")]
        [Produces("application/json")]
        public async Task<ActionResult<Demo>> Get(string id)
        {
            var demo = await _demoService.Get(id);

            if (demo == null)
            {
                return NotFound();
            }

            return demo;
        }
    }
}