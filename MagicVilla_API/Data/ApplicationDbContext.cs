using MagicVilla_Api.Models;
using MagicVilla_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Villa>? Villas { get; set; }
        public DbSet<VillaNumber>? NumberVillas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Villa Marina",
                    Detail = "Vistas al mar",
                    UrlImage = "",
                    Occupants = 5,
                    SquareMeters = 50,
                    Amenity = "",
                    DateCreation = DateTime.Now,
                    DateUpdated = DateTime.Now,
                },
                                new Villa()
                                {
                                    Id = 2,
                                    Name = "Villa Piscina",
                                    Detail = "Vistas a la piscina",
                                    UrlImage = "",
                                    Occupants = 4,
                                    SquareMeters = 45,
                                    Amenity = "",
                                    DateCreation = DateTime.Now,
                                    DateUpdated = DateTime.Now,
                                }
            );
        }


    }
}