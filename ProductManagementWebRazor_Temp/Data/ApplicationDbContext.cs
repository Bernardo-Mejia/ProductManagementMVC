using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProductManagementWebRazor_Temp.Models;

namespace ProductManagementWebRazor_Temp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Science", DisplayOrder = 1, CreatedAt=DateTime.Now },
                new Category { Id = 2, Name = "History", DisplayOrder = 2, CreatedAt=DateTime.Now },
                new Category { Id = 3, Name = "Math", DisplayOrder = 3, CreatedAt=DateTime.Now }
            );
        }
    }
}
