namespace MobileStore.Infrastructure.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;  
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
    }
}
