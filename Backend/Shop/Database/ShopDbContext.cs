using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Shop.Model;

namespace Shop.Database
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {
           // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Category> Categories { get; set; } 
        public DbSet<Feature> Features { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<FeatureValue> FeatureValues { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //}
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{            
        //}
    }
}
