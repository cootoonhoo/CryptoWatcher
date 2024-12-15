using CryptoWatcher.Core.Entities;
using CryptoWatcher.Core.Interface;
using CryptoWatcher.Core.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWatcher.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class CryptoWatcherController : ControllerBase
    {
        private readonly IExternalAPIService _externalAPIService;
        private readonly IProfileService _profileService;

        public CryptoWatcherController(IExternalAPIService externalAPIService, IProfileService profileService)
        {
            _externalAPIService = externalAPIService;
            _profileService = profileService;

        }

        [HttpGet("CurrentPrice", Name = "GetCurrentPrice")]
        public async Task<ActionResult<CryptoInfo>> GetCurrentPrice([FromHeader] string symbol)
        {
            CryptoInfo cryptoInfo = await _externalAPIService.GetCurrentPrice(symbol);
            return Ok(cryptoInfo);
        }

        [HttpGet("Test", Name = "Test")]
        public IActionResult GetTestEntity()
        {
            bool entityTest = _profileService.Test();
            return Ok(entityTest);

        }
    }
}