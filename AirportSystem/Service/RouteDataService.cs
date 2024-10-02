using AirportSystem.Data;
using AirportSystem.Models.Dto;
using AirportSystem.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Route = AirportSystem.Models.Route;

namespace AirportSystem.Service
{

    public class RouteDataService : IRouteDataService
    {
        private readonly AirportDbContext _context;
        public RouteDataService(AirportDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RouteDto>> GetRoutes()
        {
            return await _context.Routes
                .Include(r => r.DepartureAirport)
                .Include(r => r.ArrivalAirport)
                .Select(x => new RouteDto
                {
                    RouteID = x.RouteID,
                    DepartureAirport = x.DepartureAirport!,
                    ArrivalAirport = x.ArrivalAirport!
                })
                .ToListAsync();
        }        

        public async Task AddRoute(Route route)
        {
            _context.Routes.Add(route);
            await _context.SaveChangesAsync();
        }

        public async Task AddRoutesForGroup(List<Route> addRoutes)
        {
            await _context.Routes.AddRangeAsync(addRoutes);
            await _context.SaveChangesAsync();
        }    

        public async Task<bool> RouteExists(int departureAirportId, int arrivalAirportId)
        {
            return await _context.Routes
                   .AnyAsync(r =>
                    r.DepartureAirportID == departureAirportId &&
                    r.ArrivalAirportID == arrivalAirportId);
        }
    }
}
