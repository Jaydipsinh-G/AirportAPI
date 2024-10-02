using AirportSystem.Models;

namespace AirportSystem.Service.Interface
{
    public interface IAirportGroupDataService
    {
        Task<IEnumerable<AirportGroup>> GetAirportGroups();
        Task<AirportGroup?> GetAirportGroupById(int id);
        Task<AirportGroup> AddAirportGroup(string name);
        Task<bool> GroupExists(int groupId);
        Task<List<int>> GetAirportsInGroup(int value);
    }
}
