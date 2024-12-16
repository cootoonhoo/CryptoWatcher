using CryptoWatcher.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWatcher.Core.Interface.Services
{
    public interface IProfileService
    {
        public bool DeleteProfile(string profileName);
        public Profile GetProfile(string profileName);
        public List<Profile> GetAllProfiles();

        public Task<Profile> UpsertProfile(string profileName, List<string> cryptos);


    }
}
