using Countries.Contracts.DTOs;
using Countries.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Aplication
{
    public interface ICountriesAppService
    {
        public Task<List<Country>> GetAllAsync();
        public Task<List<Country>> AndFilter(string name, string initials, string currency);

        public Task<List<Country>> CreateRoute(string country1, string country2);
    }
}
