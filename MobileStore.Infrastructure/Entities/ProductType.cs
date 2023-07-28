namespace MobileStore.Infrastructure.Entities
{
    public class ProductType
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        // to get list of products by type
        public List<Product> Products { get; set; } = null!;
    }
}
