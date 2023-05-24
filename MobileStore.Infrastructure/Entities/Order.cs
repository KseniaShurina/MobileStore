namespace MobileStore.Infrastructure.Entities
{
    /// <summary>
    /// Order - the class that will represent the order
    /// </summary>
    public class Order
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
