using CryptoWatcher.Core.Entities;

namespace CryptoWatcher.Core.Interface
{
    public interface IExternalAPIService
    {
        public Task<CryptoInfo> GetCurrentPrice(string symblo);
    }
}
