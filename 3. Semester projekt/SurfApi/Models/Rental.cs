using Surf.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace SurfApi.Models
{
    public class Rental
    {
        
        public int RentalId { get; set; }

        public string UserId { get; set; }
        public int SurfboardId { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime EndDate { get; set; } 

        //public ApplicationUser User { get; set; }   
        public Surfboard Surfboard { get; set; }
        
    }
}
