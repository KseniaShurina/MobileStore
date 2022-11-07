using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileStore.Infrastructure.Contexts;
using MobileStore.Presentation.Controllers.Base;
using MobileStore.Presentation.ViewModels;

namespace MobileStore.Presentation.Controllers
{
    public class HomeController : MvcControllerBaseSecure
    {
        private readonly DefaultContext _context;

        public HomeController(DefaultContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index([FromQuery] int? productTypeId)
        {
            var productView = new ProductsViewModel();

            productView.ProductTypes = await _context.ProductTypes.ToListAsync();

            if (!productTypeId.HasValue)
            {
                // Если никакое значение типа не передаётся, то по дефолту выводит последний эл списка типов
                productTypeId = productView.ProductTypes.FirstOrDefault()?.Id;
            }

            // достаём из БД продукты по типу
            productView.Products = await _context.Products
                .AsNoTracking()
                .Where(i => i.ProductTypeId == productTypeId)
                .ToListAsync();
            return View(productView);
        }

        public async Task<IActionResult> Buy(int id)
        {
            var product = await _context.Products.Where(i => i.Id == id).FirstOrDefaultAsync();
            return View(product);
        }
    }
}