using System.ComponentModel.DataAnnotations;

namespace MobileStore.Presentation.Api.Models.Product
{
    public class ProductDto
    {
        public Guid Id { get; init; }
        public Guid ProductTypeId { get; init; }
        public string Name { get; init; } = null!;
        public string Company { get; init; } = null!;
        public double Price { get; init; }

       // public List<ProductContentDto> Contents { get; set; } = null!;
    }
}
