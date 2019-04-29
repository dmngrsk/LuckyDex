using LuckyDex.Api.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LuckyDex.Api.Controllers
{
    [Route("images")]
    [ApiController]
    public class ImagesController : Controller
    {
        private readonly IImageRepository _repository;

        public ImagesController(IImageRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var image = await _repository.GetAsync(id);

            return image == null ? (ActionResult) NotFound() : File(image.Bytes, "image/png");
        }
    }
}
