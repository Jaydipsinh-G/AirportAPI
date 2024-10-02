using AirportSystem.Service.Interface;
using AirportSystem.Validator.Interface;
using Route = AirportSystem.Models.Route;

namespace AirportSystem.Validator
{
    public class RouteValidator : IRouteValidator
    {
        private readonly IAirportDataService _airportService;
        private readonly IRouteDataService _routeService;

        public RouteValidator(IRouteDataService routeService, IAirportDataService airportService)
        {
            _routeService = routeService;
            _airportService = airportService;
        }
        public async Task<bool> ValidateRoute(Route route)
        {
            var departureAirportExists = await _airportService.DepartureAirportExists(route.DepartureAirportID);
            var arrivalAirportExists = await _airportService.ArrivalAirportExists(route.ArrivalAirportID);

            var routeExists = await _routeService.RouteExists(route.DepartureAirportID, route.ArrivalAirportID);

            if (!departureAirportExists || !arrivalAirportExists || routeExists || route.DepartureAirportID == route.ArrivalAirportID)
                return false;

            return true;
        }    
    }
}
