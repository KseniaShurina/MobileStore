using System.ComponentModel.DataAnnotations;

namespace MobileStore.Infrastructure.Entities
{
    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
        public double ProductPrice { get; set; }
    }
}
