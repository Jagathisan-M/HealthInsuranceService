using Microsoft.Extensions.Caching.Memory;

namespace HealthInsuranceAPI.AuthendicationService
{
    public class MemoryCacheService
    {
        IMemoryCache cache;
        public MemoryCacheService(IMemoryCache _cache) {
            cache = _cache;
        }

        public void StoreToken(string ID, string token)
        {
            cache.Set($"token_{ID}", token, TimeSpan.FromHours(30));
        }

        public string? GetToken(string ID)
        {
            cache.TryGetValue($"token_{ID}", out string? token);
            return token;
        }

        public void RevokeToken(string ID) {
            cache.Remove($"token_{ID}");
        }
    }
}
