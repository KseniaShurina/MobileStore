namespace MobileStore.Presentation.Api.Models.Product
{
    public class ProductContentDto
    {
        public Guid Id { get; init; }
        public Guid ContentId { get; init; }

        public string ContentType { get; init; } = null!;
        public string Name { get; init; } = null!;

        public Guid ProductId { get; init; }
        public ProductDto Product { get; init; } = null!;
    }
}
