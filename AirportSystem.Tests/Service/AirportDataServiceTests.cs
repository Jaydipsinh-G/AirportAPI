using AirportSystem.Data;
using AirportSystem.Models;
using AirportSystem.Service;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace AirportSystem.Tests.Services
{
    public class AirportDataServiceTests
    {
        private readonly AirportDbContext _dbContext;
        private readonly AirportDataService _airportService;

        public AirportDataServiceTests()
        {
            var options = new DbContextOptionsBuilder<AirportDbContext>()
                .UseInMemoryDatabase(databaseName: "AirportTestDatabase")
                .Options;

            _dbContext = new AirportDbContext(options);
            _airportService = new AirportDataService(_dbContext);
        }

        [Fact]
        public async Task GetAirports_ShouldReturnListOfAirports()
        {
            // Arrange
            var airports = new List<Airport>
            {
                new Airport { AirportID = 6, IATACode = "AAA", GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 6, Name = "Country1" }, Type = "Departure and Arrival" },
                new Airport { AirportID = 7, IATACode = "BBB", GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 7, Name = "Country2" }, Type = "Arrival Only" },
                new Airport { AirportID = 8, IATACode = "CCC", GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 8, Name = "Country3" }, Type = "Arrival Only" }
            };
            await _dbContext.Airports.AddRangeAsync(airports);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _airportService.GetAirports();

            // Assert
            Assert.NotNull(result);           
            Assert.Contains(result, a => a.Id == 6 && a.IATACode == "AAA");
            Assert.Contains(result, a => a.Id == 7 && a.IATACode == "BBB");
            Assert.Contains(result, a => a.Id == 8 && a.IATACode == "CCC");
        }

        [Fact]
        public async Task GetAirportById_ShouldReturnAirport()
        {
            // Arrange
            var airport = new Airport
            {
                AirportID = 5,
                IATACode = "XXXX",
                GeographyLevel1ID = 4,
                GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 4, Name = "Test4 Country" },
                Type = "Departure and Arrival"
            };
            await _dbContext.Airports.AddAsync(airport);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _airportService.GetAirportById(5);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test4 Country", result.GeographyLevel1.Name);
        }

        [Fact]
        public async Task DepartureAirportExists_ShouldReturnTrue_WhenDepartureAirportExists()
        {
            // Arrange
            var departureAirportId = 3;
            var airport = new Airport
            {
                AirportID = departureAirportId,
                IATACode = "XXXX",
                GeographyLevel1ID = 2,
                GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 2, Name = "Test1 Country" },
                Type = "Departure and Arrival"
            };
            await _dbContext.Airports.AddAsync(airport);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _airportService.DepartureAirportExists(departureAirportId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DepartureAirportExists_ShouldReturnFalse_WhenDepartureAirportDoesNotExist()
        {
            // Arrange
            var departureAirportId = 1;

            // Act
            var result = await _airportService.DepartureAirportExists(departureAirportId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DepartureArrivalAirportExists_ShouldReturnTrue_WhenArrivalAirportExists()
        {
            // Arrange
            var arrivalAirportId = 4;
            var airport = new Airport
            {
                AirportID = arrivalAirportId,
                Type = "Departure and Arrival",
                IATACode = "XXXX",
                GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 3, Name = "Test2 Country" }
            };
            await _dbContext.Airports.AddAsync(airport);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _airportService.ArrivalAirportExists(arrivalAirportId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ArrivalAirportExists_ShouldReturnTrue_WhenArrivalAirportExists()
        {
            // Arrange
            var arrivalAirportId = 1;
            var airport = new Airport
            {
                AirportID = arrivalAirportId,
                Type = "Arrival Only",
                IATACode = "XXXX",
                GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 1, Name = "Test Country" }
            };            
            await _dbContext.Airports.AddAsync(airport);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _airportService.ArrivalAirportExists(arrivalAirportId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ArrivalAirportExists_ShouldReturnFalse_WhenArrivalAirportDoesNotExist()
        {
            // Arrange
            var arrivalAirportId = 11;

            // Act
            var result = await _airportService.ArrivalAirportExists(arrivalAirportId);

            // Assert
            Assert.False(result);
        }
    }
}
