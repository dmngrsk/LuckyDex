using Microsoft.AspNetCore.Mvc;

namespace LuckyDex.Front.Controllers
{
    [Route("api/cookies")]
    public class LuckyDexCookiesController : Controller
    {
        [HttpGet("add-cookie/{name}")]
        public ActionResult<string> AddCookie(string name)
        {
            Response.Cookies.Append(name, "1");

            return Ok();
        }
    }
}
