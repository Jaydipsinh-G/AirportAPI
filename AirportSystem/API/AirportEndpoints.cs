using AirportSystem.Service.Interface;
using Microsoft.OpenApi.Models;

namespace AirportSystem.API
{
    public static class AirportEndpoints
    {
        public static void MapAirportEndpoints(this IEndpointRouteBuilder app)
        {         

            //Scenario 1 - Viewing all Airports
            
            app.MapGet("/airports", async (IAirportDataService airportService) =>
            {
                var airports = await airportService.GetAirports();
                return Results.Ok(airports);
            })
            .WithName("GetAirports")
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Get all Airports information",
                Description = "Returns information about all Airports.",
                Tags = new List<OpenApiTag> { new() { Name = "Airports" } }
            });

            //Scenario 2 - Viewing a Single Airport

            app.MapGet("/airports/{airportId:int}", async (int airportId, IAirportDataService airportService) =>
            {
                var airport = await airportService.GetAirportById(airportId);
                if (airport is null)
                {
                    return Results.NotFound($"Airport with ID {airportId} not found.");
                }
                return Results.Ok(airport);
            })
            .WithName("GetAirportById")
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Get Airport information By Id",
                Description = "Returns information about a selected Airport.",
                Tags = new List<OpenApiTag> { new() { Name = "Airports" } }
            });            
        }
    }
}
