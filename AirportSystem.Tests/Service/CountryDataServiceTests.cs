using AirportSystem.Data;
using AirportSystem.Models;
using AirportSystem.Service;
using AirportSystem.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace AirportSystem.Tests.Services
{
    public class CountryDataServiceTests
    {
        private readonly AirportDbContext _dbContext;
        private readonly ICountryDataService _countryService;

        public CountryDataServiceTests()
        {
            var options = new DbContextOptionsBuilder<AirportDbContext>()
                .UseInMemoryDatabase(databaseName: "CountryTestDatabase")
                .Options;

            _dbContext = new AirportDbContext(options);
            _countryService = new CountryDataService(_dbContext);
        }

        [Fact]
        public async Task GetCountries_ShouldReturnListOfCountries()
        {
            // Arrange
            var countries = new List<GeographyLevel1>
            {
                new GeographyLevel1 { GeographyLevel1ID = 1, Name = "Country1" },
                new GeographyLevel1 { GeographyLevel1ID = 2, Name = "Country2" },
                new GeographyLevel1 { GeographyLevel1ID = 3, Name = "Country3" }
            };
            await _dbContext.GeographyLevels.AddRangeAsync(countries);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _countryService.GetCountries();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            Assert.Contains(result, c => c.GeographyLevel1ID == 1 && c.Name == "Country1");
            Assert.Contains(result, c => c.GeographyLevel1ID == 2 && c.Name == "Country2");
            Assert.Contains(result, c => c.GeographyLevel1ID == 3 && c.Name == "Country3");
        }

        [Fact]
        public async Task AddCountry_ShouldAddCountry()
        {
            // Arrange
            var countryName = "New Country";

            // Act
            var result = await _countryService.AddCountry(countryName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(countryName, result.Name);
        }

        [Fact]
        public async Task AddCountry_ShouldNotAddExistingCountry()
        {
            // Arrange
            var existingCountry = new GeographyLevel1 { Name = "Existing Country" };
            await _dbContext.GeographyLevels.AddAsync(existingCountry);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _countryService.AddCountry(existingCountry.Name);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteCountry_ShouldRemoveCountry()
        {
            // Arrange
            var country = new GeographyLevel1
            {
                GeographyLevel1ID = 7,
                Name = "Country to Delete"
            };
            await _dbContext.GeographyLevels.AddAsync(country);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _countryService.DeleteCountry(country.GeographyLevel1ID);

            // Assert
            Assert.True(result);
            Assert.Null(await _dbContext.GeographyLevels.FindAsync(country.GeographyLevel1ID));
        }

        [Fact]
        public async Task DeleteCountry_ShouldNotRemoveNonExistingCountry()
        {
            // Arrange
            var nonExistingCountryId = 10;

            // Act
            var result = await _countryService.DeleteCountry(nonExistingCountryId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task IsUsedByAirport_ShouldReturnTrueIfCountryIsUsedByAirport()
        {
            // Arrange
            var countryId = 1;
            var airport = new Airport
            {
                GeographyLevel1ID = countryId,
                IATACode = "ABC",
                Type = "International"
            };

            await _dbContext.Airports.AddAsync(airport);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _countryService.IsUsedByAirport(countryId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsUsedByAirport_ShouldReturnFalseIfCountryIsNotUsedByAirport()
        {
            // Arrange
            var countryId = 1;

            // Act
            var result = await _countryService.IsUsedByAirport(countryId);

            // Assert
            Assert.False(result);
        }
    }
}
