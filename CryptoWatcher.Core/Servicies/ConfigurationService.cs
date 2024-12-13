using CryptoWatcher.Core.Interface;
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
        public string GetBaseUrlAPI()
        {
            string baseUrl = _configuration["ExternalApiUrl:BaseUrl"];
            if (baseUrl == null)
                throw new ArgumentNullException();
            return baseUrl;
        }
    }
}
