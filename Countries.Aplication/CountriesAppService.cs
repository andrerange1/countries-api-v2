using AutoMapper;
using Countries.Contracts.DTOs;
using Countries.Domain;
using Countries.Domain.Entities;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Aplication
{
    public class CountriesAppService : ICountriesAppService
    {
        private readonly ICountriesService _service;
        

        public CountriesAppService(ICountriesService service)
        {
            _service = service;
        }

        public async Task<List<Country>> GetAllAsync()
        {
            return await _service.GetAllAsync();
        }

        public async Task<List<Country>> AndFilter(string name, string initials, string currency)
        {
            return await _service.AndFilter(name, initials, currency);    
        }

        public Task<List<Country>> CreateRoute(string country1, string country2)
        {
            return _service.CreateRoute(country1, country2);
        }
    }
}
