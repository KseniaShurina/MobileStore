using MobileStore.Presentation.Mvc.Models;

namespace MobileStore.Presentation.Mvc.ViewModels
{
    public class IndexProductsViewModel
    {
        public List<ProductDto> Products { get; set; } = new();
        public List<ProductTypeDto> ProductTypes { get; set; } = new();
    }
}
