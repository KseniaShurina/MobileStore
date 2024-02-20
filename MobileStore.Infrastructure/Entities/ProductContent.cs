namespace MobileStore.Infrastructure.Entities
{
    public class ProductContent
    {
        public Guid Id { get; set; }
        public Guid ContentId { get; set; }

        public string ContentType { get; init; } = null!;
        public string Name { get; init; } = null!;

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
