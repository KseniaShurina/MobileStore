using System.Collections.Generic;

namespace MobileStore.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; } //для того чтобы получить весь список продуктов по типу
    }
}
