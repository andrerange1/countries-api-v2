using Countries.Domain.Entities;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Domain
{
    public interface ICountriesService
    {
        public Task<List<Country>> GetAllAsync();
        public List<Country> FilterByName(string name, List<Country> countries);
        public List<Country> FilterByInitials(string initials, List<Country> countries);
        public List<Country> FilterByCurrency(string currency, List<Country> countries);
        public Task<List<Country>> AndFilter(string name, string initials, string currency);
        public Task<List<Country>> CreateRoute(string country1, string country2);

    }
}
