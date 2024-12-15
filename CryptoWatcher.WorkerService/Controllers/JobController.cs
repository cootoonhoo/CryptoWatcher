using CryptoWatcher.Core.Entities;
using CryptoWatcher.Core.Interface.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace CryptoWatcher.WorkerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IProfileJobService _profileJobService;
        private readonly ICryptoInfoJobService _cryptoInfoJobService;
        public JobController(IProfileJobService profileJobService, ICryptoInfoJobService cryptoInfoJobService)
        {
              _cryptoInfoJobService = cryptoInfoJobService;
            _profileJobService = profileJobService;
        }


        [HttpPost("UpsertProfile", Name = "UpsertProfile")]
        public ActionResult<Profile> UpsertProfile(Profile profile)
        {
            _profileJobService.UpsertJob(profile);
            return Ok("Job Scheduled");
        }

        [HttpPost("UpsertCyptoInfo", Name = "UpsertCyptoInfo")]
        public ActionResult<CryptoInfo> UpsertCyptoInfo(CryptoInfo crypto)
        {
            _cryptoInfoJobService.UpsertJob(crypto);
            return Ok("Job Scheduled");
        }
    }
}
