using System.ComponentModel.DataAnnotations;

namespace MobileStore.Infrastructure.Entities
{
    public class ProductType
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        // to get list of products by type
        public List<Product> Products { get; set; } = null!;
    }
}
