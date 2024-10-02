using AirportSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Route = AirportSystem.Models.Route;

namespace AirportSystem.Data
{
    public class AirportDbContext(DbContextOptions<AirportDbContext> options) : DbContext(options)
    {
        public DbSet<Airport> Airports { get; set; }
        public DbSet<GeographyLevel1> GeographyLevels { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<AirportGroup> AirportGroups { get; set; }
        public DbSet<AirportGroupAirport> AirportGroupAirports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AirportGroupAirport>()
                .HasKey(aga => new { aga.AirportGroupID, aga.AirportID });

            modelBuilder.Entity<AirportGroupAirport>()
            .HasOne(aga => aga.AirportGroup)
            .WithMany(ag => ag.AirportGroupAirports)
            .HasForeignKey(aga => aga.AirportGroupID);

            modelBuilder.Entity<AirportGroupAirport>()
                .HasOne(aga => aga.Airport)
                .WithMany()
                .HasForeignKey(aga => aga.AirportID);

            // Seed data for GeographyLevel1
            modelBuilder.Entity<GeographyLevel1>().HasData(
                new GeographyLevel1 { GeographyLevel1ID = 1, Name = "United Kingdom" },
                new GeographyLevel1 { GeographyLevel1ID = 2, Name = "Spain" },
                new GeographyLevel1 { GeographyLevel1ID = 3, Name = "United States" },
                new GeographyLevel1 { GeographyLevel1ID = 4, Name = "Turkey" }
            );

            // Seed data for Airport
            modelBuilder.Entity<Airport>().HasData(
                new Airport { AirportID = 1, IATACode = "LGW", GeographyLevel1ID = 1, Type = "Departure and Arrival" },
                new Airport { AirportID = 2, IATACode = "PMI", GeographyLevel1ID = 2, Type = "Arrival Only" },
                new Airport { AirportID = 3, IATACode = "LAX", GeographyLevel1ID = 3, Type = "Arrival Only" }
            );

            // Seed data for Route
            modelBuilder.Entity<Route>().HasData(
                new Route { RouteID = 1, DepartureAirportID = 1, ArrivalAirportID = 2 }
            );

            // Seed data for AirportGroup
            modelBuilder.Entity<AirportGroup>().HasData(
                new AirportGroup { AirportGroupID=1, Name = "Group A" },
                new AirportGroup { AirportGroupID=2, Name = "Group B" }
                );

            // Seed data for AirportGroupAirport
            modelBuilder.Entity<AirportGroupAirport>().HasData(
               new AirportGroupAirport { AirportGroupID = 1, AirportID = 1 },
               new AirportGroupAirport { AirportGroupID = 1, AirportID = 2 },
               new AirportGroupAirport { AirportGroupID = 2, AirportID = 3 }
               );          

        }
    }
}
