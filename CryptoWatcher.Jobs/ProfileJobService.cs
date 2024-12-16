using CryptoWatcher.Core.Entities;
using CryptoWatcher.Core.Interface.Repositories;
using CryptoWatcher.Core.Interface.Services;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWatcher.Jobs
{
    public class ProfileJobService : IProfileJobService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public ProfileJobService(IProfileRepository profileRepository, IBackgroundJobClient backgroundJobClient)
        {
            _profileRepository = profileRepository;
            _backgroundJobClient = backgroundJobClient;
        }

        public void UpsertJob(Profile entity)
        {
            if (entity == null)
                return;
            var x = (_profileRepository.GetByName(entity.ProfileName));
            if (_profileRepository.GetByName(entity.ProfileName) != null)
            {
                _backgroundJobClient.Enqueue(() => _profileRepository.Update(entity));
                return;
            }

            _backgroundJobClient.Enqueue(() => _profileRepository.Save(entity));
        }
    }
}
