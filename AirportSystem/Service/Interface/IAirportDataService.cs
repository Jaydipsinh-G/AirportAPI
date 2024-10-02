using AirportSystem.Models;
using AirportSystem.Models.Dto;

namespace AirportSystem.Service.Interface
{
    public interface IAirportDataService
    {
        Task<IEnumerable<AirportDto>> GetAirports();
        Task<Airport?> GetAirportById(int id);
        Task<bool> DepartureAirportExists(int departureAirportId);
        Task<bool> ArrivalAirportExists(int arrivalAirportId);
    }
}
