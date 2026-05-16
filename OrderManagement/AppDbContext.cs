using Microsoft.EntityFrameworkCore;
using OrderManagement.Model;

namespace OrderManagement
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<County> Countries { get; set; }
    }
}
