using Ardalis.GuardClauses;

namespace MobileStore.Core.Models
{
    public class UserRegisterModel
    {
        public string Email { get; }

        public string Password { get; }

        public UserRegisterModel(string email, string password)
        {
            Email = Guard.Against.NullOrEmpty(email);
            Password = Guard.Against.NullOrEmpty(password);
        }
    }
}
