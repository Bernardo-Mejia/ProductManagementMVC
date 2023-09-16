using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;

namespace ProductManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", CreatedAt = DateTime.Now, DisplayOrder = 1 },
                new Category { Id = 2, Name = "Science", CreatedAt = DateTime.Now, DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", CreatedAt = DateTime.Now, DisplayOrder = 3 }
                );
            //base.OnModelCreating(modelBuilder);
        }
    }
}
