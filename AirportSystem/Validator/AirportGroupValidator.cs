using AirportSystem.Service.Interface;
using AirportSystem.Validator.Interface;

namespace AirportSystem.Validator
{


    public class AirportGroupValidator : IAirportGroupValidator
    {
        private readonly IAirportGroupDataService _airportGroupService;

        public AirportGroupValidator(IAirportGroupDataService airportGroupService)
        {
            _airportGroupService = airportGroupService;
        }

        public async Task<bool> ValidateAirportGroupIds(int departureGroupId, int arrivalGroupId)
        {
            bool departureGroupExists = await _airportGroupService.GroupExists(departureGroupId);
            bool arrivalGroupExists = await _airportGroupService.GroupExists(arrivalGroupId);

            return departureGroupExists && arrivalGroupExists;
        }
    }
}
