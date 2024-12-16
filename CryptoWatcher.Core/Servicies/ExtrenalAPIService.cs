using CryptoWatcher.Core.Entities;
using CryptoWatcher.Core.Interface;
using CryptoWatcher.Core.Interface.Repositories;
using CryptoWatcher.Core.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoWatcher.Core.Servicies
{
    public class ExtrenalAPIService : IExternalAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfigurationService _configurationService;
        private readonly IProfileRepository _profileRepository;
        private readonly ICryptoInfoRepository _cryptoInfoRepository;
        public ExtrenalAPIService(HttpClient httpClient, IConfigurationService configurationService, ICryptoInfoRepository cryptoInfoRepository, IProfileRepository profileRepository)
        {
            _httpClient = httpClient;
            _configurationService = configurationService;
            _cryptoInfoRepository = cryptoInfoRepository;
            _profileRepository = profileRepository;
        }
        public async Task<CryptoInfo> GetCurrentPrice(string symbol)
        {
            var a = $"{_configurationService.GetBaseExternalUrlAPI()}/price?symbol={symbol}";
                var response = await _httpClient.GetAsync($"{_configurationService.GetBaseExternalUrlAPI()}/price?symbol={symbol}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                CryptoInfo result = JsonSerializer.Deserialize<CryptoInfo>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result == null)
                    return null;

                return result;
        }

        public async Task<CryptoInfo> UpsertCrypto(CryptoInfo crypto)
        {
            var content = new StringContent(JsonSerializer.Serialize(crypto),Encoding.UTF8,"application/json");
            var response = await _httpClient.PostAsync($"{_configurationService.GetBaseInternalUrlAPI()}/UpsertCyptoInfo", content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CryptoInfo>(responseBody);
        }

        public async Task<Profile> UpsertProfile(Profile profile)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

            var content = new StringContent(JsonSerializer.Serialize(profile), Encoding.UTF8, "application/json");
            var x = $"{_configurationService.GetBaseInternalUrlAPI()}/UpsertProfile";
            var response = await _httpClient.PostAsync($"{_configurationService.GetBaseInternalUrlAPI()}/UpsertProfile", content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Profile>(responseBody);
        }
    }
}
