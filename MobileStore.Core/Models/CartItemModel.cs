namespace MobileStore.Core.Models;

public class CartItemModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public ProductModel Product { get; set; } = null!;
    public int Quantity { get; set; }
}