using Ardalis.GuardClauses;

namespace MobileStore.Core.Models
{
    public class OrderCreateModel
    {
        public string Email { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string ContactPhone { get; private set; }

        public string Address { get; private set; }


        public OrderCreateModel(string email, string firstName, string lastName, string contactPhone, string address)
        {
            Email = Guard.Against.NullOrEmpty(email);
            FirstName = Guard.Against.NullOrEmpty(firstName);
            LastName = Guard.Against.NullOrEmpty(lastName);
            ContactPhone = Guard.Against.NullOrEmpty(contactPhone);
            Address = Guard.Against.NullOrEmpty(address);
        }
    }
}
