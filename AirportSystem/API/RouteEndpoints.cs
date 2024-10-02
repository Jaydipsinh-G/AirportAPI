using AirportSystem.Models;
using AirportSystem.Service.Interface;
using AirportSystem.Validator.Interface;
using Microsoft.OpenApi.Models;
using Route = AirportSystem.Models.Route;

namespace AirportSystem.API
{
    public static class RouteEndpoints
    {
        public static void MapRouteEndpoints(this IEndpointRouteBuilder app)
        {
            //Scenario 1 - Viewing All Routes

            app.MapGet("/routes", async (IRouteDataService routeService) =>
            {
                var routes = await routeService.GetRoutes();
                return Results.Ok(routes);
            })
            .WithName("GetRoutes")
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Get all Airport Routes information",
                Description = "Returns information about all Routes.",
                Tags = new List<OpenApiTag> { new() { Name = "Routes" } }
            });


            //Scenario 2 - Adding a New Route using Airport IDs or Airport Group IDs

            app.MapPost("/routes", async (CreateRouteRequest request,
                              IRouteValidator routeValidator,
                              IRouteDataService routeService,
                              IAirportGroupValidator groupValidator,
                              IAirportGroupDataService groupService) =>
            {
                // Case 1: Handling Airport IDs
                if (request.DepartureAirportID.HasValue && request.ArrivalAirportID.HasValue)
                {
                    var route = new Route
                    {
                        DepartureAirportID = request.DepartureAirportID.Value,
                        ArrivalAirportID = request.ArrivalAirportID.Value
                    };

                    if (!await routeValidator.ValidateRoute(route))
                    {
                        return Results.BadRequest("Invalid route: either airport does not exist or the route is invalid.");
                    }
                    // Save the route
                    await routeService.AddRoute(route);
                    return Results.Ok("Route created with airports.");
                }

                // Case 2: Handling Airport Group IDs
                if (request.DepartureAirportGroupID.HasValue && request.ArrivalAirportGroupID.HasValue)
                {
                    if (!await groupValidator.ValidateAirportGroupIds(request.DepartureAirportGroupID.Value, request.ArrivalAirportGroupID.Value))
                    {
                        return Results.BadRequest("Invalid airport group IDs.");
                    }

                    // Get airports in both groups
                    var departureAirports = await groupService.GetAirportsInGroup(request.DepartureAirportGroupID.Value);
                    var arrivalAirports = await groupService.GetAirportsInGroup(request.ArrivalAirportGroupID.Value);

                    // Create routes for each pair of airports from the two groups
                    var routes = new List<Route>();
                    foreach (var departure in departureAirports)
                    {
                        foreach (var arrival in arrivalAirports)
                        {
                            var newRoute = new Route
                            {
                                DepartureAirportID = departure,
                                ArrivalAirportID = arrival
                            };
                            // Validate and add the route
                            if (await routeValidator.ValidateRoute(newRoute))
                            {
                                routes.Add(newRoute);
                            }
                        }
                    }
                    // Save the routes
                    await routeService.AddRoutesForGroup(routes);
                    return Results.Ok("Routes created using airport groups.");
                }

                return Results.BadRequest("Either airport IDs or airport group IDs must be provided.");
            })
            .WithName("AddRoute")
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Add a new Route",
                Description = "Adds a new Route to the airport routes list. The route can be added using either airport IDs or airport group IDs.",
                Tags = new List<OpenApiTag> { new() { Name = "Routes" } }
            });

        }
    }
}
