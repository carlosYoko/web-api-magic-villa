using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Api.Models

{
    public class Villa
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Detail { get; set; }

        [Required]
        public double Fee { get; set; }

        public int Occupants { get; set; }

        public int SquareMeters { get; set; }

        public string? UrlImage { get; set; }

        public string? Amenity { get; set; }

        public DateTime DateCreation { get; set; }

        public DateTime DateUpdated { get; set; }

    }
}