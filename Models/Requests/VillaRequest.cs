using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_DB.Models.Requests
{
    public class VillaRequest
    {
        [Required(ErrorMessage = "Name dibutuhkan")]
        [MaxLength(100, ErrorMessage = "Maksimal karakter Name 100")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Details dibutuhkan")]
        [MaxLength(255, ErrorMessage = "Maximal karakter Details 255")]
        public string Details { get; set; }

        [Required]
        public Guid TownId { get; set; }

        public double Rate { get; set; }

        public int Occupancy { get; set; }

        public string ImageUrl { get; set; }

        public string Amenity { get; set; }
    }
}
