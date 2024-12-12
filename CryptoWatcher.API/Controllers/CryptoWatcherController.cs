using Microsoft.AspNetCore.Mvc;

namespace CryptoWatcher.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CryptoWatcherController : ControllerBase
    {
        

        [HttpGet(Name = "GetStatus")]
        public IActionResult Get()
        {
            return Ok("API Test");
        }
    }
}
