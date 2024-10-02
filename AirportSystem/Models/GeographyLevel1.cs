using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportSystem.Models
{
    [Table("GeographyLevel1")]
    public class GeographyLevel1
    {
        [Key]
        public int GeographyLevel1ID { get; set; }
        public required string Name { get; set; }
    }
}
