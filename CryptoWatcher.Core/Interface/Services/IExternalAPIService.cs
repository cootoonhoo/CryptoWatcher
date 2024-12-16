using CryptoWatcher.Core.Entities;

namespace CryptoWatcher.Core.Interface
{
    public interface IExternalAPIService
    {
        public Task<CryptoInfo> GetCurrentPrice(string symblo);

        public Task<Profile> UpsertProfile(Profile profile);
        public Task<CryptoInfo> UpsertCrypto(CryptoInfo crypto);
    }
}
