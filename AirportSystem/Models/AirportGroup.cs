using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportSystem.Models
{
    [Table("AirportGroup")]
    public class AirportGroup
    {
        [Key]
        public int AirportGroupID { get; set; }
        public required string Name { get; set; }
        public ICollection<AirportGroupAirport> AirportGroupAirports { get; set; } = new List<AirportGroupAirport>();
    }    
}
