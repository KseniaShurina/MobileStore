using System;
using Ardalis.GuardClauses;

namespace MobileStore.Infrastructure.Entities
{
    public class Product
    {
        public Guid Id { get; init; }
        public Guid ProductTypeId { get; set; }
        public ProductType ProductType { get; init; } = null!;
        public string Name { get; set; } = null!;
        public string Company { get; set; } = null!;
        public double Price { get; set; }


        // по этому свойству можно узнать сколько раз этот продукт заказывали например
        public List<CartItem> CartItems { get; set; } = null!;
        public List<OrderItem> OrderItems { get; set; } = null!;

        public List<ProductContent> Contents { get; set; } = null!;
    }
}
