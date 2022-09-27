using AutoMapper;
using Countries.Aplication;
using Countries.Contracts.DTOs;
using Countries.Domain.Entities;
using Countries.Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace Countries.Api.Controllers
{
    [Route("api/countries")]
    [ApiController]
    [Produces("application/json")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesAppService _service;
        private readonly IMapper _mapper;

        public CountriesController(ICountriesAppService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        ///(Questão 1A) List all countries.
        /// </summary>
        /// <returns>List all countries</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Country>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var countries = await _service.GetAllAsync();
            return Ok(countries); 
        }

        /// <summary>
        /// (Questão 1A)  List all countries names.
        /// </summary>
        /// <returns>List all countries names</returns>
        [HttpGet("names")]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllNamesAsync()
        {
            var countries = await _service.GetAllAsync();

            var result = new List<string>();

            foreach(var country in countries)
                result.Add(country.Name);

            return Ok(result);
        }

        /// <summary>
        ///  (Questão 1C)    Filter Countries by name and initials and currency. In case of no parameters returns the entire list.
        /// </summary>
        /// <returns></returns>
        /// <param name="name"></param>
        /// <param name="initials"></param>
        /// <param name="currency"></param>
        [HttpGet("filter")]
        [ProducesResponseType(typeof(List<CountryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> FilterByNameOrInitialsOrCurrency(string? name, string? initials, string? currency)
        {
            var countries = await _service.AndFilter(name, initials, currency);
            var result = _mapper.Map<List<CountryResponse>>(countries);
            return Ok(result); 
        }

        /// <summary>
        ///  (Questão 1D)    Filter Countries by name and initials and currency. In case of no parameters returns the entire list.
        /// </summary>
        /// <returns></returns>
        /// <param name="name"></param>
        /// <param name="initials"></param>
        /// <param name="currency"></param>
        [HttpGet("filterDetails")]
        [ProducesResponseType(typeof(List<CountryDetailResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DetailFilterByNameOrInitialsOrCurrency(string? name, string? initials, string? currency)
        {
            var countries = await _service.AndFilter(name, initials, currency);
            var result = _mapper.Map<List<CountryDetailResponse>>(countries);
            return Ok(result); 
        }

        /// <summary>
        /// (Questão 2) Creates the best route between two given countries.
        /// </summary>
        /// <returns></returns>
        [HttpGet("createroute")]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateRoute(string origin, string destination)
        {
            var countries = await _service.CreateRoute(origin, destination);

            List<string> routes = new List<string>();

            foreach (var country in countries)
                routes.Add(country.Name);

            return Ok(routes); 
        }



        
    }
}
