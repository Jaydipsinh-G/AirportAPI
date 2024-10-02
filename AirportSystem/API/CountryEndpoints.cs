using AirportSystem.Service.Interface;
using Microsoft.OpenApi.Models;

namespace AirportSystem.API
{
    public static class CountryEndpoints
    {
        public static void MapCountryEndpoints(this IEndpointRouteBuilder app)
        {
            //Scenario 1 - Viewing All Countries

            app.MapGet("/countries", async (ICountryDataService countryService) =>
            {
                var countries = await countryService.GetCountries();
                return Results.Ok(countries);
            })
            .WithName("GetCountries")
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Get all Countries information",
                Description = "Returns information about all Countries.",
                Tags = new List<OpenApiTag> { new() { Name = "Countries" } }
            });

            //Scenario 2 - Adding a Country

            app.MapPost("/countries", async (string name, ICountryDataService countryService) =>
            {
                var country = await countryService.AddCountry(name);
                if (country is null)
                {
                    return Results.BadRequest("Country already exists.");
                }
                return Results.Created($"/countries/{country.GeographyLevel1ID}", country);
            })
            .WithName("AddCountry")
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Add a new Country",
                Description = "Adds a new Country to the Countries list.",
                Tags = new List<OpenApiTag> { new() { Name = "Countries" } }
            });


            //Scenario 3 - Deleting a Country

            app.MapDelete("/countries/{id:int}", async (int id, ICountryDataService countryService) =>
            {
                var isUsedByAirport = await countryService.IsUsedByAirport(id);
                if (isUsedByAirport)
                {
                    return Results.BadRequest("Country is in use for an airport and cannot be deleted.");
                }

                var success = await countryService.DeleteCountry(id);
                return success ? Results.NoContent() : Results.NotFound($"Country with ID {id} not found.");
            })
            .WithName("DeleteCountry")
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Delete a Country",
                Description = "Deletes a Country from the Countries list.",
                Tags = new List<OpenApiTag> { new() { Name = "Countries" } }
            });
        }
    }
}
