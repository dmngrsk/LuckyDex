using LuckyDex.Api.Interfaces.Repositories;
using LuckyDex.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LuckyDex.Api.Controllers
{
    [Route("trainers")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly ITrainerRelationshipRepository _repository;

        public TrainersController(ITrainerRelationshipRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet("{name}")]
        public async Task<ActionResult<TrainerRelationship>> GetAsync(string name)
        {
            try
            {
                var trainer = await _repository.GetAsync(name);

                return trainer == null ? (ActionResult<TrainerRelationship>) NotFound() : Ok(trainer);
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

                await _repository.PutAsync(value);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
