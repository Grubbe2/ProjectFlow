using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surf.Models
{
    public class Surfboard
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*")]
        public double Length { get; set; }
        [Required(ErrorMessage = "*")]
        public double  Width { get; set; }
        [Required(ErrorMessage = "*")]
        public double Thickness { get; set; }
        [Required(ErrorMessage = "*")]
        public double Volume { get; set; }
        [Required(ErrorMessage = "*")]
      
        public decimal Price { get; set; }
        [Required(ErrorMessage = "*")]
        public string Type { get; set; }
        
        public string? Equipment { get; set; }
        [Required(ErrorMessage = "*")]
        public string Image { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public bool IsRented { get; set; } = false; 

    }
}
