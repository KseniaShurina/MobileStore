namespace MobileStore.Core.Models
{
    public class ProductModel
    {
        public int Id { get; init; }
        public int ProductTypeId { get; init; }
        public string Name { get; init; } = null!;
        public string Company { get; init; } = null!;
        public double Price { get; init; }
        public string Img { get; init; } = null!;
    }
}
