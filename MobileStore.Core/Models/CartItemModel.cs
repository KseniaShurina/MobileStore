namespace MobileStore.Core.Models;

public class CartItemModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public ProductModel Product { get; set; }
    public int Quantity { get; set; }
}