using System;
using LuckyDex.Api.Interfaces.Repositories;
using LuckyDex.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LuckyDex.Api.Controllers
{
    [Route("pokemon")]
    [ApiController]
    public class PokémonController : ControllerBase
    {
        private readonly IPokémonRelationshipRepository _relationshipRepository;

        public PokémonController(IPokémonRelationshipRepository relationshipRepository)
        {
            _relationshipRepository = relationshipRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PokémonRelationship>> GetAsync(string id)
        {
            try
            {
                var pokémon = await _relationshipRepository.GetAsync(id);

                return pokémon == null ? (ActionResult<PokémonRelationship>) NotFound() : Ok(pokémon);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
