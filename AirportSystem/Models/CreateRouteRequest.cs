namespace AirportSystem.Models
{
    public class CreateRouteRequest
    {
        public int? DepartureAirportID { get; set; }
        public int? ArrivalAirportID { get; set; }
        public int? DepartureAirportGroupID { get; set; }
        public int? ArrivalAirportGroupID { get; set; }
    }
}
