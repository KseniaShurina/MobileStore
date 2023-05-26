using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Models
{
    internal class OrderModel
    {
        public int Id { get; set; }
        public DateTime Datetime { get; set; }
        public string Address { get; set; } = null!;
        public string ContactPhone { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public List<OrderItem> Items { get; set; } = null!;
    }
}
