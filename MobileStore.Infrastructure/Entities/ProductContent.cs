namespace MobileStore.Infrastructure.Entities
{
    public class ProductContent
    {
        public Guid Id { get; init; }
        public Guid ContentId { get; init; }
        public Guid ProductId { get; init; }
        public Product Product { get; set; } = null!;
        public string ContentType { get; init; } = null!;
        public string Name { get; init; } = null!;
    }
}
