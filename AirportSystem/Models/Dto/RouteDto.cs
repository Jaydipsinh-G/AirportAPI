namespace AirportSystem.Models.Dto
{
    public class RouteDto
    {
        public int RouteID { get; set; }
        public required Airport DepartureAirport { get; set; }
        public required Airport ArrivalAirport { get; set; }
    }
}
