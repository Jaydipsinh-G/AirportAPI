using Route = AirportSystem.Models.Route;

namespace AirportSystem.Validator.Interface
{
    public interface IRouteValidator
    {
        Task<bool> ValidateRoute(Route route);
    }
}
