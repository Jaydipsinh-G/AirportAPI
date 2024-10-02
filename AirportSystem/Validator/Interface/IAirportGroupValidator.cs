namespace AirportSystem.Validator.Interface
{
    public interface IAirportGroupValidator
    {
        Task<bool> ValidateAirportGroupIds(int departureGroupId, int arrivalGroupId);
    }
}
