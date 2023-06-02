using System.ComponentModel.DataAnnotations;

namespace MobileStore.Infrastructure.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; } //для того чтобы получить весь список продуктов по типу
    }
}
