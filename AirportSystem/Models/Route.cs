using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportSystem.Models
{
    [Table("Route")]
    public class Route
    {
        [Key]
        public int RouteID { get; set; }
        public int DepartureAirportID { get; set; }
        public int ArrivalAirportID { get; set; }
        public Airport? DepartureAirport { get; set; }
        public Airport? ArrivalAirport { get; set; }
    } 
}
