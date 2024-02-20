namespace MobileStore.Core.Models
{
    public class ProductModel
    {
        public Guid Id { get; init; }
        public Guid ProductTypeId { get; init; }
        public string Name { get; init; } = null!;
        public string Company { get; init; } = null!;
        public double Price { get; init; }
        public IReadOnlyCollection<ProductContentModel> Contents { get; set; } = null!;
    }
}
