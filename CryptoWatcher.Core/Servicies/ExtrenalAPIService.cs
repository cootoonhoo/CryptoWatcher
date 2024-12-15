using CryptoWatcher.Core.Entities;
using CryptoWatcher.Core.Interface;
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
        private IConfigurationService _configurationService;
        public ExtrenalAPIService(HttpClient httpClient, IConfigurationService configurationService)
        {
            _httpClient = httpClient;
            _configurationService = configurationService;
        }
        public async Task<CryptoInfo> GetCurrentPrice(string symbol)
        {
            try
            {
                var req = $"{_configurationService.GetBaseUrlAPI()}/price?symbol={symbol}";
                var response = await _httpClient.GetAsync($"{_configurationService.GetBaseUrlAPI()}/price?symbol={symbol}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                CryptoInfo result = JsonSerializer.Deserialize<CryptoInfo>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (result == null)
                    throw new NullReferenceException();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
