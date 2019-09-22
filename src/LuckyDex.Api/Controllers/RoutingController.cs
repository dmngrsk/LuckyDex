using LuckyDex.Api.Interfaces.Repositories;
using LuckyDex.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LuckyDex.Api.Controllers
{
    [Route("routing")]
    [ApiController]
    public class RoutingController : ControllerBase
    {
        private readonly IRoutingRepository _routingRepository;

        public RoutingController(IRoutingRepository dexEntryRepository)
        {
            _routingRepository = dexEntryRepository;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Routing>> GetAsync(string name)
        {
            try
            {
                var routing = await _routingRepository.GetAsync(name);

                return Ok(routing);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
