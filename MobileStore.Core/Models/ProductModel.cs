using MobileStore.Infrastructure.Entities;

namespace MobileStore.Core.Models
{
    public class ProductModel
    {
        public Guid Id { get; init; }
        public Guid ProductTypeId { get; init; }
        public string Name { get; init; } = null!;
        public string Company { get; init; } = null!;
        public double Price { get; init; }
        public string Img { get; init; } = null!;
        public Content Content { get; set; } = null!;
    }
}
