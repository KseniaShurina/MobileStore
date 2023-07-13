namespace MobileStore.Core.Models
{
    public class OrderCreateModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string ContactPhone { get; set; } = null!;
        public string? Address { get; set; } = null!;
    }
}
