using MobileStore.Presentation.Models;

namespace MobileStore.Presentation.ViewModels
{
    public class IndexProductsViewModel
    {
        public List<ProductDto> Products { get; set; } = new();
        public List<ProductTypeDto> ProductTypes { get; set; } = new();
    }
}
