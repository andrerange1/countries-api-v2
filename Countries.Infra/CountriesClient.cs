using Countries.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Countries.Infra
{
    public class CountriesClient : ICountriesClient
    {
        private const string COUNTRIES_KEY = "Countries";
        //(Questão 1B) dados salvos em memoria cache na primeira requisição e consultados lá a partir de então.
        public async Task <List<Country>> GetCountriesIntoCacheMemoryAsync(IMemoryCache cache, string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                var responseData = await response.Content.ReadAsStringAsync();
                var countries = JsonConvert.DeserializeObject<List<Country>>(responseData);

                var memoryCacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
                    SlidingExpiration = TimeSpan.FromSeconds(1200)
                };

                cache.Set(COUNTRIES_KEY, countries, memoryCacheEntryOptions);

                return countries;

            }
        }
    }
}
