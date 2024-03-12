using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Api.Models.Dto
{
    public class VillaUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }

        public string? Detail { get; set; }

        [Required]
        public double Fee { get; set; }

        [Required]
        public int Occupants { get; set; }

        [Required]
        public int SquareMeters { get; set; }

        [Required]
        public string? UrlImage { get; set; }

        public string? Amenity { get; set; }
    }
}