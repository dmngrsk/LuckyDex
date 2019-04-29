using LuckyDex.Api.Interfaces.Repositories;
using LuckyDex.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LuckyDex.Api.Controllers
{
    [Route("pokemon")]
    [ApiController]
    public class PokémonController : ControllerBase
    {
        private readonly IPokémonRepository _repository;

        public PokémonController(IPokémonRepository repository)
        {
            _repository = repository;
        }

        public async Task<ActionResult<IReadOnlyCollection<Pokémon>>> GetAsync([FromQuery] bool? isTradeable, [FromQuery] bool? isLowestForm, [FromQuery] bool? isLegendary)
        {
            var pokémon = await _repository.GetManyAsync(isTradeable, isLowestForm, isLegendary);

            return Ok(pokémon);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pokémon>> GetAsync(int id)
        {
            var pokémon = await _repository.GetAsync(id);

            return pokémon == null ? (ActionResult<Pokémon>)NotFound() : Ok(pokémon);
        }

        [HttpPut("{id}")]
        public async Task PutAsync(int id, [FromBody] Pokémon value)
        {
            await _repository.PutAsync(id, value);
        }
    }
}
