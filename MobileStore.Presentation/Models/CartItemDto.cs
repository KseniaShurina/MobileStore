namespace MobileStore.Presentation.Models
{
    public class CartItemDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public ProductDto Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
