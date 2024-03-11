using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Api.Models.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }

        public string? Detail { get; set; }

        [Required]
        public double Fee { get; set; }

        public int Occupants { get; set; }

        public int SquareMeters { get; set; }

        public string? UrlImage { get; set; }

        public string? Amenity { get; set; }
    }
}