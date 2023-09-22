namespace MobileStore.Core.Models
{
    public class ProductTypeModel
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public List<ProductModel> Products { get; init; } = null!;
    }
}
