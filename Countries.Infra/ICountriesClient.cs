using Countries.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Countries.Infra
{
    public interface ICountriesClient
    {
        Task<List<Country>> GetCountriesIntoCacheMemoryAsync(IMemoryCache cache, string url);
    }
}