using CryptoWatcher.Core.Entities;
using CryptoWatcher.Core.Interface;
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
    public class CryptoInfoJobService : ICryptoInfoJobService
    {
        private readonly ICryptoInfoRepository _cryptoInfoRepository;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IExternalAPIService _externalAPIService;


        public CryptoInfoJobService(ICryptoInfoRepository cryptoInfoRepository, IBackgroundJobClient backgroundJobClient, IExternalAPIService externalAPIService)
        {
            _cryptoInfoRepository = cryptoInfoRepository;
            _backgroundJobClient = backgroundJobClient;
            _externalAPIService = externalAPIService;
        }
        public async void UpdateCryptoCurrencyHourly()
        {
            string multipleSymbol = String.Empty;
            List <CryptoInfo> cryptos = _cryptoInfoRepository.GetAll();
            CryptoInfo aux;
            foreach (CryptoInfo crypto in cryptos)
            {
                aux = await _externalAPIService.GetCurrentPrice(crypto.Symbol);
                if (aux == null)
                    continue;
                _backgroundJobClient.Enqueue(() => _cryptoInfoRepository.Update(aux));
            }
        }

        public void UpsertJob(CryptoInfo entity)
        {
            if (entity == null)
                return;

            if (_cryptoInfoRepository.Get(entity.Id) != null)
            {
                _backgroundJobClient.Enqueue(() => _cryptoInfoRepository.Update(entity));
                return;
            }

            _backgroundJobClient.Enqueue(() => _cryptoInfoRepository.Save(entity));
        }
    }
}
