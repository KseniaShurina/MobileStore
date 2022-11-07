using System.Collections.Generic;
using MobileStore.Infrastructure.Models;

namespace MobileStore.Presentation.ViewModels
{
    /// <summary>
    /// Передает из метода Index продукты и типы по View
    /// </summary>
    public class ProductsViewModel
    {
        public List<Product> Products { get; set; } = new();
        public List<ProductType> ProductTypes { get; set; } = new();
    }
}
