using MobileStore.Presentation.Mvc.Models;

namespace MobileStore.Presentation.Mvc.ViewModels
{
    /// <summary>
    /// Передает из метода Index продукты и типы по View
    /// Send from method Index products and types by View
    /// </summary>
    public class ProductsViewModel
    {
        public List<ProductDto> Products { get; set; } = new(); 
        public List<ProductTypeDto> ProductTypes { get; set; } = new();
    }
}
