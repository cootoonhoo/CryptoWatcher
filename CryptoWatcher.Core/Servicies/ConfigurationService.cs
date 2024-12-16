using CryptoWatcher.Core.Interface.Services;
using Microsoft.Extensions.Configuration;

namespace CryptoWatcher.Core.Servicies
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;
        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetBaseInternalUrlAPI() =>
            _configuration["ApiUrl:InternalJobAPI"];


        public string GetBaseExternalUrlAPI() =>
            _configuration["ApiUrl:BinanceApi"];
    }
}
