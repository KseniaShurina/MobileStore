using System.ComponentModel.DataAnnotations;

namespace MobileStore.Infrastructure.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Company { get; set; } = null!;
        public double Price { get; set; }
        public string Img { get; set; } = null!;
    }
}
