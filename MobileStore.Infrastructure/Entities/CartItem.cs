using System.ComponentModel.DataAnnotations;

namespace MobileStore.Infrastructure.Entities
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
