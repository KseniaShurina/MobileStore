namespace MobileStore.Core.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public DateTime Datetime { get; set; }
        public string Address { get; set; } = null!;
        public string ContactPhone { get; set; } = null!;

        public Guid UserId { get; set; }

        public List<OrderItemModel> Items { get; set; } = null!;
    }
}
