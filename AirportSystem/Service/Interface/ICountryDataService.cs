using AirportSystem.Models;

namespace AirportSystem.Service.Interface
{
    public interface ICountryDataService
    {
        Task<IEnumerable<GeographyLevel1>> GetCountries();
        Task<GeographyLevel1?> AddCountry(string name);
        Task<bool> DeleteCountry(int id);
        Task<bool> IsUsedByAirport(int id);
    }
}
