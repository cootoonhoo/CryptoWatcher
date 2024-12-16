using CryptoWatcher.Core.Entities;
using CryptoWatcher.Core.Interface;
using CryptoWatcher.Core.Interface.Repositories;
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
        private readonly ICryptoInfoJobService _cryptoInfoJobService;



        public CryptoWatcherController(IExternalAPIService externalAPIService, IProfileService profileService, ICryptoInfoRepository cryptoInfoRepository)
        {
            _externalAPIService = externalAPIService;
            _profileService = profileService;
        }

        [HttpGet("CurrentPrice", Name = "GetCurrentPrice")]
        public async Task<ActionResult<CryptoInfo>> GetCurrentPrice( string symbol)
        {
            CryptoInfo cryptoInfo = await _externalAPIService.GetCurrentPrice(symbol);
            return Ok(cryptoInfo);
        }
        [HttpGet("GetProfile", Name = "GetProfile")]
        public ActionResult<Profile> GetProfile(string profileName)
        {
            Profile profile = _profileService.GetProfile(profileName);
            if (profile == null) return NotFound();
            return Ok(profile);
        }
        [HttpPost("UpsertProfile", Name = "UpsertProfile")]
        public async Task<ActionResult<Profile>> UpsertProfile( string profileName, List<string> cryptoSymbols)
        {
            Profile profile = await _profileService.UpsertProfile(profileName, cryptoSymbols);

            return profile != null? Ok(profile) : BadRequest();
        }
        [HttpDelete("DeleteProfile", Name = "DeleteProfile")]
        public ActionResult DeleteProfile(string profileName)
        {
            bool status = _profileService.DeleteProfile(profileName);
            return status? Ok(status) : NotFound();
        }
    }
}