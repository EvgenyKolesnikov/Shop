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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
           modelBuilder.Entity<Category>()
                .HasMany(p => p.Products)
                .WithOne(p => p.Category)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Category>(category =>
            {
                category.HasKey(c => c.Id);
                category.HasIndex(c => c.ParentCategoryId);

                category.HasOne(c => c.ParentCategory)
                .WithMany(c => c.ChildCategories)
                .HasForeignKey(c => c.ParentCategoryId);
            });
        }



        public void TruncateAllTables()
        {
            Database.ExecuteSqlRaw("EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL' ");
            Database.ExecuteSqlRaw("EXEC sp_MSForEachTable 'DELETE FROM ?' ");
            Database.ExecuteSqlRaw("EXEC sp_MSForEachTable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL' ");
        }
    }
}
