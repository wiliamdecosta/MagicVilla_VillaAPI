using MagicVilla_DB.Data.Stores;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_DB.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Town> Towns { get; set; }
        public DbSet<Villa> Villas { get; set; }

    }
}
