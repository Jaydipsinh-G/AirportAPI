using AirportSystem.Models.Dto;
using Route = AirportSystem.Models.Route;

namespace AirportSystem.Service.Interface
{
    public interface IRouteDataService
    {
        Task<IEnumerable<RouteDto>> GetRoutes();
        Task AddRoute(Route route);
        Task AddRoutesForGroup(List<Route> addRoutes);
        Task<bool> RouteExists(int departureAirportId, int arrivalAirportId);
    }
}
