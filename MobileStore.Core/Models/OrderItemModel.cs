namespace MobileStore.Core.Models
{
    public class OrderItemModel
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public ProductModel Product { get; set; } = null!;
        public int Quantity { get; set; }
        public double ProductPrice { get; set; }
    }
}
