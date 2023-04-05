using MobileStore.Core.Models;

namespace MobileStore.Presentation.ViewModels
{
    /// <summary>
    /// Передает из метода Index продукты и типы по View
    /// </summary>
    public class ProductsViewModel
    {
        public List<ProductModel> Products { get; set; } = new();
        public List<ProductTypeModel> ProductTypes { get; set; } = new();
    }
}
