namespace MobileStore.Models
{/// <summary>
/// Order - the class that will represent the order
/// </summary>
    public class Order
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactPhone { get; set; }
        //The last two properties together represent a foreign key to the associated Phone model.
        public int PhoneId { get; set; } // ссылка на связанную модель Phone
        public Phone Phone { get; set; }
    }
}
