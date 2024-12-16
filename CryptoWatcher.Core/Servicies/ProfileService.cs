using CryptoWatcher.Core.Entities;
using CryptoWatcher.Core.Interface;
using CryptoWatcher.Core.Interface.Repositories;
using CryptoWatcher.Core.Interface.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoWatcher.Core.Servicies
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ICryptoInfoRepository _cryptoInfoRepository;
        private readonly IExternalAPIService _externalAPIService;

        public ProfileService(IProfileRepository profileRepository, ICryptoInfoRepository cryptoInfoRepository, IExternalAPIService externalAPIService)
        {
            _profileRepository = profileRepository;
            _cryptoInfoRepository = cryptoInfoRepository;
            _externalAPIService = externalAPIService;
        }

        public bool DeleteProfile(string profileName)
        {
            Profile entity = _profileRepository.GetByName(profileName);
            if (entity == null) return false;
            return _profileRepository.Delete(entity);
        }

        public Profile GetProfile(string profileName) =>
            _profileRepository.GetByName(profileName);

        public async Task<Profile> UpsertProfile(string profileName, List<string> cryptos)
        {
            Profile newProfile = new Profile();
            newProfile.ProfileName = profileName;

            if(cryptos.Count == 0) return newProfile;

            List<CryptoInfo> lstCrypto = new List<CryptoInfo>();
            CryptoInfo aux;
            foreach (string cryptoSymbol in cryptos)
            {
                aux = _cryptoInfoRepository.GetBySymbol(cryptoSymbol);
                if (aux != null)
                {
                    lstCrypto.Add(aux);
                    continue;
                }

                aux = await _externalAPIService.GetCurrentPrice(cryptoSymbol);
                if (aux == null) continue;
                // Apperently, saving a new profile will also save cryptos
                // aux = await _externalAPIService.UpsertCrypto(aux);
                lstCrypto.Add(aux);
            }
            newProfile.Cryptos = lstCrypto;

            return await _externalAPIService.UpsertProfile(newProfile);
        }
    }
}
