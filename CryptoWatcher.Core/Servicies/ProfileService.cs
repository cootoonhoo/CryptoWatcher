using CryptoWatcher.Core.Entities;
using CryptoWatcher.Core.Interface.Repositories;
using CryptoWatcher.Core.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWatcher.Core.Servicies
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository profileRepository; 
        public ProfileService(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }
        public bool Test()
        {
            var T = new Profile();
            T.ProfileName = "Test";
            return profileRepository.Save(T);
        }
    }
}
