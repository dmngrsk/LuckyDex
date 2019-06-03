using LuckyDex.Front.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LuckyDex.Front.Controllers
{
    [Route("api/settings")]
    public class LuckyDexSettingsController : Controller
    {
        private readonly LuckyDexSettings _settings;

        public LuckyDexSettingsController(IOptions<LuckyDexSettings> clientAppSettings)
        {
            _settings = clientAppSettings.Value;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_settings);
        }
    }
}
