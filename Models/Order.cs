namespace MobileStore.Models
{/// <summary>
/// Order - the class that will represent the order
/// </summary>
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactPhone { get; set; }
        //The last two properties together represent a foreign key to the associated Phone model.
        public int ProductId { get; set; } // ссылка на связанную модель Phone
        public Product Product { get; set; }
    }
}
