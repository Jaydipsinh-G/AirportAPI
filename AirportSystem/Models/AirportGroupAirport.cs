using System.ComponentModel.DataAnnotations.Schema;

namespace AirportSystem.Models
{
    [Table("AirportGroupAirport")]
    public class AirportGroupAirport
    {
        public int AirportGroupID { get; set; }
        public int AirportID { get; set; }

        public AirportGroup AirportGroup { get; set; }
        public Airport Airport { get; set; }
    }
}
