using AirportSystem.Data;
using AirportSystem.Models;
using AirportSystem.Service;
using AirportSystem.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace AirportSystem.Tests.Services
{
    public class AirportGroupDataServiceTests
    {
        private readonly AirportDbContext _dbContext;
        private readonly IAirportGroupDataService _airportGroupDataService;

        public AirportGroupDataServiceTests()
        {
            var options = new DbContextOptionsBuilder<AirportDbContext>()
                .UseInMemoryDatabase(databaseName: "AirportGroupTestDatabase")
                .Options;

            _dbContext = new AirportDbContext(options);
            _airportGroupDataService = new AirportGroupDataService(_dbContext);
        }

        [Fact]
        public async Task AddAirportGroup_ShouldAddNewAirportGroup()
        {
            // Arrange
            var groupName = "New Group";

            // Act
            var result = await _airportGroupDataService.AddAirportGroup(groupName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(groupName, result.Name);
            Assert.NotEqual(0, result.AirportGroupID);
        }

        [Fact]
        public async Task GetAirportGroupById_ShouldReturnAirportGroup()
        {
            // Arrange
            var airportGroup = new AirportGroup
            {
                AirportGroupID = 2,
                Name = "Test Group"
            };
            await _dbContext.AirportGroups.AddAsync(airportGroup);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _airportGroupDataService.GetAirportGroupById(2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Group", result.Name);
        }
        [Fact]
        public async Task GroupExists_ShouldReturnTrue_WhenGroupExists()
        {
            // Arrange
            var groupId = 7;
            var airportGroup = new AirportGroup
            {
                AirportGroupID = groupId,
                Name = "BBB Group"
            };
            await _dbContext.AirportGroups.AddAsync(airportGroup);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _airportGroupDataService.GroupExists(groupId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAirportGroups_ShouldReturnListOfAirportGroups()
        {
            // Arrange
            var airportGroups = new List<AirportGroup>
                    {
                        new AirportGroup { AirportGroupID = 4, Name = "Group4" },
                        new AirportGroup { AirportGroupID = 5, Name = "Group5" },
                        new AirportGroup { AirportGroupID = 6, Name = "Group6" }
                    };
            await _dbContext.AirportGroups.AddRangeAsync(airportGroups);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _airportGroupDataService.GetAirportGroups();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Count());
            Assert.Contains(result, ag => ag.AirportGroupID == 4 && ag.Name == "Group4");
            Assert.Contains(result, ag => ag.AirportGroupID == 5 && ag.Name == "Group5");
            Assert.Contains(result, ag => ag.AirportGroupID == 6 && ag.Name == "Group6");
        }          

        [Fact]
        public async Task GroupExists_ShouldReturnFalse_WhenGroupDoesNotExist()
        {
            // Arrange
            var groupId = 1;

            // Act
            var result = await _airportGroupDataService.GroupExists(groupId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetAirportsInGroup_ShouldReturnListOfAirportIds()
        {
            // Arrange
            var groupId = 1;
            var airportGroup = new AirportGroup
            {
                AirportGroupID = groupId,
                Name = "Test Group",
                AirportGroupAirports = new List<AirportGroupAirport>
                {
                        new AirportGroupAirport {
                            AirportID = 1,
                            AirportGroup = new AirportGroup { AirportGroupID = groupId, Name = "Test Group" },
                            Airport = new Airport {
                                AirportID = 1,
                                IATACode = "AAA",
                                GeographyLevel1 = new GeographyLevel1 { Name = "" },
                                Type = "Type1"
                            }
                        },
                        new AirportGroupAirport {
                            AirportID = 2,
                            AirportGroup = new AirportGroup { AirportGroupID = groupId, Name = "Test Group" },
                            Airport = new Airport {
                                AirportID = 2,
                                IATACode = "BBB",
                                GeographyLevel1 = new GeographyLevel1 { Name = "" },
                                Type = "Type2"
                            }
                        },
                        new AirportGroupAirport {
                            AirportID = 3,
                            AirportGroup = new AirportGroup { AirportGroupID = groupId, Name = "Test Group" },
                            Airport = new Airport {
                                AirportID = 3,
                                IATACode = "CCC",
                                GeographyLevel1 = new GeographyLevel1 { Name = "GeographyName" },
                                Type = "Type3"
                            }
                        }
                }
            };
            await _dbContext.AirportGroups.AddAsync(airportGroup);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _airportGroupDataService.GetAirportsInGroup(groupId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Contains(result, airportId => airportId == 1);
            Assert.Contains(result, airportId => airportId == 2);
            Assert.Contains(result, airportId => airportId == 3);
        }
    }
}
