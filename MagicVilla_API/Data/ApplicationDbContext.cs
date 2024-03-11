using MagicVilla_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Villa>? Villas { get; set; }


    }
}