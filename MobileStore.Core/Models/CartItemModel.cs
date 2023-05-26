using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Models;

public class CartItemModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    //public ProductModel ProductModel { get; set; }
    public int Quantity { get; set; }
}