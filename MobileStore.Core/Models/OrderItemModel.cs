using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Models
{
    public class OrderItemModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public double ProductPrice { get; set; }
    }
}
