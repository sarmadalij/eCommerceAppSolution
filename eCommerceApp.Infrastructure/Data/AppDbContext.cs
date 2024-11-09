using eCommerceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        //db sets
        public DbSet<Product>Products { get; set; }
        public DbSet<Category>Categories { get; set; }
    }
}
