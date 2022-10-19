using System.Collections.Generic;
using MobileStore.Models;

namespace MobileStore.ViewModels
{
    /// <summary>
    /// Передает из метода Index продукты и типы по View
    /// </summary>
    public class ProductViewModel
    {
        public List<Product> Products { get; set; } = new();
        public List<ProductType> ProductTypes { get; set; } = new();
    }
}
