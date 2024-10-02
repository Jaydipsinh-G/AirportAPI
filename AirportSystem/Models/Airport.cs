using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportSystem.Models
{
    [Table("Airport")]
    public class Airport
    {
        [Key]
        public int AirportID { get; set; }
        public required string IATACode { get; set; }
        public int GeographyLevel1ID { get; set; }
        public required string Type { get; set; }

        public GeographyLevel1 GeographyLevel1 { get; set; }        
    }     
}
