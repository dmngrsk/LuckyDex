using LuckyDex.Api.Interfaces.Repositories;
using LuckyDex.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LuckyDex.Api.Controllers
{
    [Route("trainers")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly ITrainerRepository _trainerRepository;
        private readonly IDexEntryRepository _dexEntryRepository;

        public TrainersController(ITrainerRepository trainerRepository, IDexEntryRepository dexEntryRepository)
        {
            _trainerRepository = trainerRepository;
            _dexEntryRepository = dexEntryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<Trainer>>> GetAllAsync()
        {
            try
            {
                var trainers = await _trainerRepository.GetAllAsync();

                return Ok(trainers);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<TrainerRelationship>> GetAsync(string name)
        {
            try
            {
                var trainer = await _trainerRepository.GetAsync(name);
                var entries = await _dexEntryRepository.GetTrainerEntriesAsync(name);

                var relationship = new TrainerRelationship
                {
                    Trainer = trainer,
                    Pokémon = entries?.Select(e => new Pokémon { Id = e.PokémonId }).ToList()
                };

                return Ok(relationship);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
        
        [HttpPut("{name}")]
        public async Task<ActionResult> PutAsync(string name, [FromBody] TrainerRelationship value)
        {
            try
            {
                if (value.Trainer.Name != name)
                {
                    return BadRequest();
                }

                var entries = value.Pokémon.Select(p => new DexEntry { PokémonId = p.Id, TrainerName = value.Trainer.Name }).ToList();

                await _trainerRepository.PutAsync(value.Trainer);
                await _dexEntryRepository.PutTrainerEntriesAsync(entries, true);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
