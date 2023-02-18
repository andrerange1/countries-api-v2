using Countries.Domain.Entities;
using Countries.Domain.Interfaces.Repositories;

namespace Countries.Domain.Services
{
    public class CountriesService : ICountriesService
    {

        private readonly ICountryRepository _repository;

        public CountriesService(ICountryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Country>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public List<Country> FilterByName(string name, List<Country> countries)
        {
            return countries.Where(c => String.Compare(c.Name.ToLower(), name.ToLower()) == 0).ToList();
        }
        public List<Country> FilterByInitials(string initials, List<Country> countries)
        {
            return countries.Where(c => String.Compare(c.Initials.ToLower(), initials.ToLower()) == 0).ToList();
        }

        public List<Country> FilterByCurrency(string currency, List<Country> countries)
        {
            return countries.Where(country =>
                    country.Currencies != null &&
                    country.Currencies.Where(curr => 
                        String.Compare(curr.Name.ToLower(), currency.ToLower()) ==  0).Count() > 0)
                   .ToList();
        }

        public async Task<List<Country>> AndFilter(string name, string initials, string currency)
        {
            var countries = await _repository.GetAllAsync();
            List<Country> result = new List<Country>();

            bool filterByName = name == null ? false : name.Trim().Length > 0;
            bool filterByInitials = initials == null ? false : initials.Trim().Length > 0;
            bool filterByCurrency = currency == null ? false : currency.Trim().Length > 0;

            if(!filterByName && !filterByInitials && !filterByCurrency)
            {
                return countries;
            }

            if(filterByName)
            {
                result = FilterByName(name, countries);
            }

            if(filterByInitials)
            {
                result = FilterByInitials(initials, result.Count > 0 ? result: countries);
            }

            if(filterByCurrency)
            {
                result = FilterByCurrency(currency, result.Count > 0 ? result : countries);
            }
            
            return result;

        }

        List<Country> FindRoute(Country origin, Country destination, List<Country> route, List<Country> bestRoute, List<Country> db)
        {
            foreach(string border in origin.Borders)
            {
                var country = FilterByInitials(border, db).FirstOrDefault();

                if(String.Compare(country.Name.ToLower(), destination.Name.ToLower()) == 0)
                {
                    route.Add(destination);
                    return route;
                }

                var notInRoute = FilterByName(country.Name, route).Count == 0;

                if (notInRoute)
                {
                    var childRoute = route.Where(x => true).ToList();
                    childRoute.Add(country);

                    if(bestRoute.Count > 0)
                    if(childRoute.Count > bestRoute.Count) continue;

                    if(country.Borders != null)
                    {
                        childRoute = FindRoute(country, destination, childRoute, bestRoute, db);
                        var foundRoute = FilterByName(destination.Name, childRoute).Count == 1;
                        if (foundRoute)
                        {
                            bestRoute = bestRoute.Count == 0 || childRoute.Count < bestRoute.Count ? childRoute : bestRoute;
                        }       
                    }        
                }
            }

            return bestRoute;
            
        }

        public async Task<List<Country>> CreateRoute(string originDescription, string destinationDescription)
        {
            if (originDescription == null) return new List<Country>();
            if (destinationDescription == null) return new List<Country>();

            var countries = await _repository.GetAllAsync();

            var origin = FilterByName(originDescription, countries).FirstOrDefault();

            if (origin == null || origin.Borders.Count == 0)
            {
                origin = FilterByInitials(originDescription, countries).FirstOrDefault();              
                if (origin == null || origin.Borders.Count == 0) return new List<Country>();               
            }

            var destination = FilterByName(destinationDescription, countries).FirstOrDefault();

            if (destination == null || destination.Borders.Count == 0)
            {
                destination = FilterByInitials(destinationDescription, countries).FirstOrDefault();
                if (destination == null || destination.Borders.Count == 0) return new List<Country>();
            }

            var route = new List<Country>();

            route.Add(origin);
            var result = FindRoute(origin, destination, route, new List<Country>(), countries);

            result.RemoveAt(result.Count - 1);
            result.RemoveAt(0);

            return result;
        }


    }
}
