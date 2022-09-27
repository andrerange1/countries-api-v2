using Countries.Domain;
using Countries.Domain.Entities;
using Countries.Domain.Services;
using Xunit;


namespace Countries.Tests
{
    public class CountriesServiceTests
    {
        private readonly ICountriesService _service;
        public CountriesServiceTests(ICountriesService service)
        {
            _service = service;
        }      

        [Fact]
        public void GetAllAsyncTest()
        {
            var list = _service.GetAllAsync();
            Assert.NotNull(list);
        }

        [Theory]
        [InlineData("spain")]
        [InlineData("brazil")]
        public void FilterByNameTest(string name)
        {
            List<Country> countryList = new List<Country>();
            Country country = new Country { Name = "spain" };
            Country country2 = new Country { Name = "brazil" };

            countryList.Add(country);
            countryList.Add(country2);

            List<Country> result = _service.FilterByName(name, countryList);
            Assert.True(result.Count > 0);
        }

        [Theory]
        [InlineData("spain")]
        [InlineData("brazil")]
        public void FilterByNameNotFoundTest(string name)
        {
            List<Country> countryList = new List<Country>();

            Country country = new Country { Name = "guatemala" };
            Country country2 = new Country { Name = "chile" };

            countryList.Add(country);
            countryList.Add(country2);

            List<Country> result = _service.FilterByName(name, countryList);
            Assert.True(result.Count == 0);
        }
        
        [Theory]
        [InlineData("BRA")]
        [InlineData("SPA")]
        public void FilterByInitialsTest(string initials)
        {
            List<Country> countryList = new List<Country>();
            Country country = new Country { Initials = "BRA" };
            Country country2 = new Country { Initials = "SPA" };

            countryList.Add(country);
            countryList.Add(country2);

            List<Country> result = _service.FilterByInitials(initials, countryList);
            Assert.True(result.Count > 0);
        }

        [Theory]
        [InlineData("BRA")]
        [InlineData("SPA")]
        public void FilterByInitialsNotFoundTest(string initials)
        {
            List<Country> countryList = new List<Country>();

            Country country = new Country { Initials = "USA" };
            Country country2 = new Country { Initials = "CHL" };

            countryList.Add(country);
            countryList.Add(country2);

            List<Country> result = _service.FilterByInitials(initials, countryList);
            Assert.True(result.Count == 0);
        } 

        [Theory]
        [InlineData("EURO")]
        [InlineData("DOLAR")]
        [InlineData("REAL")]
        [InlineData("LIBRA")]

        public void FilterByCurrencyTest(string currency)
        {
            Currency currency1 = new Currency() { Name = "EURO" };
            Currency currency2 = new Currency() { Name = "DOLAR" };
            Currency currency3 = new Currency() { Name = "REAL" };
            Currency currency4 = new Currency() { Name = "LIBRA" };

            List<Currency> currencyList1 = new List<Currency>();
            currencyList1.Add(currency1);
            currencyList1.Add(currency2);

            List<Currency> currencyList2 = new List<Currency>();
            currencyList2.Add(currency2);
            currencyList2.Add(currency3);

            List<Currency> currencyList3 = new List<Currency>();
            currencyList3.Add(currency3);
            currencyList3.Add(currency4);

            List<Country> countryList = new List<Country>();
            countryList.Add(new Country() { Currencies = currencyList1 });
            countryList.Add(new Country() { Currencies = currencyList2 });
            countryList.Add(new Country() { Currencies = currencyList3 });

            List<Country> result = _service.FilterByCurrency(currency, countryList);

            Assert.True(result.Count > 0);
        }

        [Theory]
        [InlineData("IENES")]
        [InlineData("PESOS")]
        [InlineData("DOLAR AUSTRALIANO")]
        public void FilterByCurrencyNotFoundTest(string currency)
        {
            Currency currency1 = new Currency() { Name = "EURO" };
            Currency currency2 = new Currency() { Name = "DOLAR" };
            Currency currency3 = new Currency() { Name = "REAL" };
            Currency currency4 = new Currency() { Name = "LIBRA" };

            List<Currency> currencyList1 = new List<Currency>();
            currencyList1.Add(currency1);
            currencyList1.Add(currency2);

            List<Currency> currencyList2 = new List<Currency>();
            currencyList2.Add(currency2);
            currencyList2.Add(currency3);

            List<Currency> currencyList3 = new List<Currency>();
            currencyList3.Add(currency3);
            currencyList3.Add(currency4);

            List<Country> countryList = new List<Country>();
            countryList.Add(new Country() { Currencies = currencyList1 });
            countryList.Add(new Country() { Currencies = currencyList2 });
            countryList.Add(new Country() { Currencies = currencyList3 });

            List<Country> result = _service.FilterByCurrency(currency, countryList);

            Assert.True(result.Count == 0);
        }

        [Theory]
        [InlineData("spain","SPA","euro")]
        [InlineData("spain", "", "")]
        [InlineData("spain", "", "euro")]
        [InlineData("", "", "euro")]
        [InlineData("", "", "")]
        public async Task AndFilterTest(string name, string initials, string currency)
        {
            List<Country> result = await _service.AndFilter(name, initials, currency);
            Assert.True(result.Count > 0);
        }

        [Theory]
        [InlineData("1","","")]
        [InlineData("", "1", "")]
        [InlineData("", "", "1")]
        public async Task AndFilterBadRequestTest(string name, string initials, string currency)
        {
            List<Country> result = await _service.AndFilter(name, initials, currency);
            Assert.True(result.Count == 0);
        }


        [Theory]
        [InlineData("brazil", "united states of america")]
        [InlineData("bra", "usa")]
        [InlineData("usa", "bra")]
        public async Task CreateRouteTest(string origin, string destination)
        {
            List<Country> result = await _service.CreateRoute(origin, destination);
            Assert.True(result.Count == 7);
        }

        [Theory]
        [InlineData("", "united states of america")]
        [InlineData(null, "united states of america")]
        [InlineData("Brazil", "")]
        [InlineData("Brazil", null)]
        public async Task CreateRouteNotFoundTest(string origin, string destination)
        {
            List<Country> result = await _service.CreateRoute(origin, destination);
            Assert.True(result.Count == 0);
        }
    }
}
