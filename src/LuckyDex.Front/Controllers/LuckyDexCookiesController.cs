using LuckyDex.Front.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDex.Front.Controllers
{
    [Route("api/cookies")]
    public class LuckyDexCookiesController : Controller
    {
        [HttpGet("add-cookie/{name}")]
        public ActionResult<string> AddCookie(string name, [FromQuery] long? expires)
        {
            var options = new CookieOptions
            {
                Expires = expires.ToNullableDateTimeOffset()
            };

            Response.Cookies.Append(name, "1", options);

            return Ok();
        }
    }
}
