using Microsoft.EntityFrameworkCore;
using MobileStore.Models;

namespace MobileStore.Contexts
{
    /// <summary>
    /// This class connects the application to the database PostgreSQL.
    /// </summary>
    public class DefaultContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public  DbSet<User> Users { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }
    }
}
