namespace MobileStore.Infrastructure.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime Datetime { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string ContactPhone { get; set; } = null!;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public List<OrderItem> Items { get; set; } = null!;
    }
}
