using AirportSystem.Data;
using AirportSystem.Models;
using AirportSystem.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace AirportSystem.Service
{  

    public class CountryDataService(AirportDbContext dbContext) : ICountryDataService
    {
        private readonly AirportDbContext _dbContext = dbContext;

        public async Task<IEnumerable<GeographyLevel1>> GetCountries()
        {
            return await _dbContext.GeographyLevels.ToListAsync();
        }

        public async Task<GeographyLevel1?> AddCountry(string name)
        {
            var existingCountry = await _dbContext.GeographyLevels.FirstOrDefaultAsync(c => c.Name == name);
            if (existingCountry != null)
            {
                return null;
            }

            var newCountry = new GeographyLevel1 { Name = name };
            _dbContext.GeographyLevels.Add(newCountry);
            await _dbContext.SaveChangesAsync();

            return newCountry;
        }

        public async Task<bool> DeleteCountry(int id)
        {
            var country = await _dbContext.GeographyLevels.FindAsync(id);
            if (country == null)
            {
                return false;
            }

            _dbContext.GeographyLevels.Remove(country);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsUsedByAirport(int id)
        {
            var isUsedByAirport = await _dbContext.Airports.AnyAsync(a => a.GeographyLevel1ID == id);
            return isUsedByAirport;
        }
    }
}
