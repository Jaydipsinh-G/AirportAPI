using AirportSystem.Data;
using AirportSystem.Models;
using AirportSystem.Service;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace AirportSystem.Tests.Services
{
    public class RouteDataServiceTests
    {
        private readonly AirportDbContext _dbContext;
        private readonly RouteDataService _routeService;

        public RouteDataServiceTests()
        {
            var options = new DbContextOptionsBuilder<AirportDbContext>()
                .UseInMemoryDatabase(databaseName: "RouteTestDatabase")
                .Options;

            _dbContext = new AirportDbContext(options);
            _routeService = new RouteDataService(_dbContext);
        }

        [Fact]
        public async Task GetRoutes_ShouldReturnListOfRoutes()
        {
            // Arrange
            var routes = new List<Route>
            {
                new Route { RouteID = 1, DepartureAirportID = 1, ArrivalAirportID = 2, DepartureAirport = new Airport { AirportID = 1, IATACode = "AAA", Type = "Departure", GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 1, Name = "Country1" } }, ArrivalAirport = new Airport { AirportID = 2, IATACode = "BBB", Type = "Arrival", GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 2, Name = "Country2" } } },
                new Route { RouteID = 2, DepartureAirportID = 3, ArrivalAirportID = 4, DepartureAirport = new Airport { AirportID = 3, IATACode = "CCC", Type = "Departure", GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 3, Name = "Country3" } }, ArrivalAirport = new Airport { AirportID = 4, IATACode = "DDD", Type = "Arrival", GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 4, Name = "Country4" } } }
            };
            await _dbContext.Routes.AddRangeAsync(routes);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _routeService.GetRoutes();

            // Assert
            Assert.NotNull(result);            
            Assert.Contains(result, r => r.RouteID == 1 && r.DepartureAirport.IATACode == "AAA" && r.ArrivalAirport.IATACode == "BBB");
            Assert.Contains(result, r => r.RouteID == 2 && r.DepartureAirport.IATACode == "CCC" && r.ArrivalAirport.IATACode == "DDD");
        }

        [Fact]
        public async Task AddRoute_ShouldAddRoute()
        {
            // Arrange
            var route = new Route
            {
                RouteID = 3,
                DepartureAirportID = 5,
                ArrivalAirportID = 6,
                DepartureAirport = new Airport { AirportID = 5, IATACode = "EEE", Type = "Departure", GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 5, Name = "Country5" } },
                ArrivalAirport = new Airport { AirportID = 6, IATACode = "FFF", Type = "Arrival", GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 6, Name = "Country6" } }
            };

            // Act
            await _routeService.AddRoute(route);
            var result = await _dbContext.Routes.FindAsync(3);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.DepartureAirportID);
            Assert.Equal(6, result.ArrivalAirportID);
        }

        [Fact]
        public async Task AddRoutesForGroup_ShouldAddMultipleRoutes()
        {
            // Arrange
            var routes = new List<Route>
            {
                new Route { RouteID = 4, DepartureAirportID = 7, ArrivalAirportID = 8, DepartureAirport = new Airport { AirportID = 7, IATACode = "GGG", Type = "Departure", GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 7, Name = "Country7" } }, ArrivalAirport = new Airport { AirportID = 8, IATACode = "HHH", Type = "Arrival", GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 8, Name = "Country8" } } },
                new Route { RouteID = 5, DepartureAirportID = 9, ArrivalAirportID = 10, DepartureAirport = new Airport { AirportID = 9, IATACode = "III", Type = "Departure", GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 9, Name = "Country9" } }, ArrivalAirport = new Airport { AirportID = 10, IATACode = "JJJ", Type = "Arrival", GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 10, Name = "Country10" } } }
            };

            // Act
            await _routeService.AddRoutesForGroup(routes);
            var result = await _dbContext.Routes.ToListAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count); 
        }

        [Fact]
        public async Task RouteExists_ShouldReturnTrue_WhenRouteExists()
        {
            // Arrange
            var route = new Route
            {
                RouteID = 6,
                DepartureAirportID = 11,
                ArrivalAirportID = 12,
                DepartureAirport = new Airport { AirportID = 11, IATACode = "KKK", Type = "Departure", GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 11, Name = "Country11" } },
                ArrivalAirport = new Airport { AirportID = 12, IATACode = "LLL", Type = "Arrival", GeographyLevel1 = new GeographyLevel1 { GeographyLevel1ID = 12, Name = "Country12" } }
            };
            await _dbContext.Routes.AddAsync(route);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _routeService.RouteExists(11, 12);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RouteExists_ShouldReturnFalse_WhenRouteDoesNotExist()
        {
            // Act
            var result = await _routeService.RouteExists(13, 14);

            // Assert
            Assert.False(result);
        }
    }
}
