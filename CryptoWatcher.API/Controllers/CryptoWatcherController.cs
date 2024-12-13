using CryptoWatcher.Core.Entities;
using CryptoWatcher.Core.Interface;
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
        public CryptoWatcherController(IExternalAPIService externalAPIService)
        {
            _externalAPIService = externalAPIService;
        }

        [HttpGet(Name = "GetCurrentPrice")]
        public async Task<ActionResult<CryptoInfo>> GetCurrentPrice([FromHeader]string symbol)
        {
            CryptoInfo cryptoInfo = await _externalAPIService.GetCurrentPrice(symbol);
            return Ok(cryptoInfo);
        }
    }
}
