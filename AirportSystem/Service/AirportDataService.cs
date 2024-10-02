using AirportSystem.Data;
using AirportSystem.Models;
using AirportSystem.Models.Dto;
using AirportSystem.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace AirportSystem.Service
{
    public class AirportDataService(AirportDbContext context) : IAirportDataService
    {
        private readonly AirportDbContext _context = context;

        public async Task<IEnumerable<AirportDto>> GetAirports()
        {
            return await _context.Airports
                .Select(a => new AirportDto { Id = a.AirportID, IATACode = a.IATACode })
                .ToListAsync();
        }

        public async Task<Airport?> GetAirportById(int id)
        {
            return await _context.Airports
                .FindAsync(id);
        }       

        public async Task<bool> DepartureAirportExists(int departureAirportId)
        {
            return await _context.Airports
                    .Where(x => x.Type == "Departure and Arrival")
                    .AnyAsync(x => x.AirportID == departureAirportId);
        }

        public async Task<bool> ArrivalAirportExists(int arrivalAirportId)
        {
            return await _context.Airports
                    .Where(x => x.Type == "Departure and Arrival" || x.Type == "Arrival Only")
                    .AnyAsync(x => x.AirportID == arrivalAirportId);
        }
    }
}
