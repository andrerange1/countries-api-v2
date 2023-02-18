using Countries.Domain.Entities;
using Countries.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Countries.Infra.Repository
{
    public class CountryRepository : ICountryRepository
    {

        private readonly ICountriesClient _countriesClient;

        private readonly IMemoryCache _cache;
        private const string COUNTRIES_KEY = "Countries";
        private const string restCountriesUrl = "https://restcountries.com/v2/all";


        public CountryRepository(ICountriesClient countriesClient, IMemoryCache cache)
        {
            _countriesClient = countriesClient;
            _cache = cache; 
        }

        public async Task<List<Country>> GetAllAsync()
        {
            if (_cache.TryGetValue(COUNTRIES_KEY, out List<Country> countries))
            {
                return countries;
            }

            countries = await _countriesClient.GetCountriesIntoCacheMemoryAsync(_cache, restCountriesUrl);

            return countries;
        }

    }
}
