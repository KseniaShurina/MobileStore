using Microsoft.EntityFrameworkCore;
using MobileStore.Infrastructure.Abstractions.Contexts;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Infrastructure.Contexts
{
    /// <summary>
    /// This class connects the application to the database PostgreSQL.
    /// </summary>
    internal class DefaultContext : DbContext, IDefaultContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductType> ProductTypes { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public  DbSet<User> Users { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<OrderItem> OrderItem { get; set; } = null!;


        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
