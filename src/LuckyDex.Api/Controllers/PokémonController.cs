using LuckyDex.Api.Interfaces.Repositories;
using LuckyDex.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LuckyDex.Api.Controllers
{
    [Route("pokemon")]
    [ApiController]
    public class PokémonController : ControllerBase
    {
        private readonly IDexEntryRepository _dexEntryRepository;

        public PokémonController(IDexEntryRepository dexEntryRepository)
        {
            _dexEntryRepository = dexEntryRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PokémonRelationship>> GetAsync(string id)
        {
            try
            {
                var entries = await _dexEntryRepository.GetPokémonEntriesAsync(id);

                var relationship = new PokémonRelationship
                {
                    Pokémon = new Pokémon { Id = id },
                    Trainers = entries.Select(e => new Trainer {Name = e.TrainerName}).ToList()
                };

                return Ok(relationship);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
