using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;

namespace ProductManagement.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", CreatedAt = DateTime.Now, DisplayOrder = 1 },
                new Category { Id = 2, Name = "Science", CreatedAt = DateTime.Now, DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", CreatedAt = DateTime.Now, DisplayOrder = 3 }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, CategoryId = 9, Author = "Humberto Eco", Description = "Valiéndose de las características propias de la novela gótica; la crónica medieval y la novela policíaca; el nombre de la rosa narra las actividades detectivescas de Guillermo de Baskerville; quien busca esclarecer los crímenes cometidos en una abadía benedictina en el año 1327.", ISBN = "978-6073103008", Title = "Nombre de la Rosa", ListPrice = 55, Price = 50, Price50 = 50, Price100 = 35 },
                new Product { Id = 2, CategoryId = 4, Author = "Gabriel García Márquez", Description = "Muchos años después, frente al pelotón de fusilamiento, el coronel Aureliano Buendía había de recordar aquella tarde remota en que su padre lo llevó a conocer el hielo.", ISBN = "978-6070728792", Title = "Cien años de soledad", ListPrice = 40, Price = 30, Price50 = 25, Price100 = 20 }
            );

            //base.OnModelCreating(modelBuilder);
        }
    }
}
