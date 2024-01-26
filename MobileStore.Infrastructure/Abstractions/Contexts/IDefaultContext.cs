using Microsoft.EntityFrameworkCore;
using MobileStore.Infrastructure.Abstractions.Contexts.Base;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Infrastructure.Abstractions.Contexts;

public interface IDefaultContext : IDbContextBase
{
    DbSet<Product> Products { get; }
    DbSet<ProductType> ProductTypes { get; }
    DbSet<Order> Orders { get; }
    DbSet<User> Users { get; }
    DbSet<CartItem> CartItems { get; }
    DbSet<OrderItem> OrderItems { get; }
    DbSet<Content> Contents { get; }
    DbSet<ProductContent> ProductContents { get; }
}