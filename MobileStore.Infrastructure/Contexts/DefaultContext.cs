using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MobileStore.Infrastructure.Abstractions.Contexts;
using MobileStore.Infrastructure.Entities;
using MobileStore.Infrastructure.EntityConfigurations;
using System.Data;
using System.Data.Common;

namespace MobileStore.Infrastructure.Contexts
{
    /// <summary>
    /// This class connects the application to the database PostgresSQL.
    /// </summary>
    internal class DefaultContext : DbContext, IDefaultContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductType> ProductTypes { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public  DbSet<User> Users { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<OrderItem> OrderItem { get; set; } = null!;
        public DbSet<Content> Contents { get; set; } = null!;
        public DbSet<ProductContent> ProductContents { get; set; } = null!;


        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurations
            modelBuilder.ApplyConfiguration(new CartItemConfigurations());
            modelBuilder.ApplyConfiguration(new ContentConfigurations());
            modelBuilder.ApplyConfiguration(new OrderConfigurations());
            modelBuilder.ApplyConfiguration(new OrderItemConfigurations());
            modelBuilder.ApplyConfiguration(new ProductConfigurations());
            modelBuilder.ApplyConfiguration(new ProductContentConfigurations());
            modelBuilder.ApplyConfiguration(new ProductTypeConfigurations());
            modelBuilder.ApplyConfiguration(new UserConfigurations());
        }


        public DbConnection GetDbConnection()
        {
            return Database.GetDbConnection();
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return Database.BeginTransactionAsync(cancellationToken);
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            return Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        }
    }
}
