using AirportSystem.Data;
using AirportSystem.Models;
using AirportSystem.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace AirportSystem.Service
{
    public class AirportGroupDataService(AirportDbContext context) : IAirportGroupDataService
    {
        private readonly AirportDbContext _context = context;

        public async Task<IEnumerable<AirportGroup>> GetAirportGroups()
        {
            return await _context.AirportGroups
                .Include(ag => ag.AirportGroupAirports)
                .ToListAsync();
        }

        public async Task<AirportGroup?> GetAirportGroupById(int id)
        {
            return await _context.AirportGroups
                .Include(ag => ag.AirportGroupAirports)
                .FirstOrDefaultAsync(ag => ag.AirportGroupID == id);
        }

        public async Task<AirportGroup> AddAirportGroup(string name)
        {
            var group = new AirportGroup { Name = name, AirportGroupAirports = new List<AirportGroupAirport>() };
            _context.AirportGroups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<bool> GroupExists(int groupId)
        {
            return await _context.AirportGroups
                .AnyAsync(g => g.AirportGroupID == groupId);
        }

        public async Task<List<int>> GetAirportsInGroup(int value)
        {
            return await _context.AirportGroups
                .Where(ag => ag.AirportGroupID == value)
                .SelectMany(ag => ag.AirportGroupAirports.Select(aga => aga.AirportID))
                .ToListAsync();
        }
    }
}
