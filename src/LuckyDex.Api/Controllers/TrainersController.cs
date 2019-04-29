using LuckyDex.Api.Interfaces.Repositories;
using LuckyDex.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LuckyDex.Api.Controllers
{
    [Route("trainers")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly ITrainerRepository _repository;

        public TrainersController(ITrainerRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet("{name}")]
        public async Task<ActionResult<Trainer>> GetAsync(string name)
        {
            var trainer = await _repository.GetAsync(name);

            return trainer == null ? (ActionResult<Trainer>) NotFound() : Ok(trainer);
        }
        
        [HttpPut("{name}")]
        public async Task PutAsync(string name, [FromBody] Trainer value)
        {
            await _repository.PutAsync(name, value);
        }
    }
}
