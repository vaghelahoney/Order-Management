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
    }
}


//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{
//    modelBuilder.Entity<Order>()
//        .HasMany(o => o.OrderItems)    
//        .WithOne(oi => oi.Order)      
//        .HasForeignKey(oi => oi.OrderId) 
//        .OnDelete(DeleteBehavior.Cascade);

//}
