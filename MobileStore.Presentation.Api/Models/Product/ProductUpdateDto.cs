using System.ComponentModel.DataAnnotations;

namespace MobileStore.Presentation.Api.Models.Product
{
    public class ProductUpdateDto
    {
        [Required] 
        public string Name { get; init; } = null!;

        [Required]
        public string Company { get; init; } = null!;

        [Required]
        public double Price { get; init; }
    }
}
