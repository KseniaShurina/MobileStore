using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MobileStore.Contexts;
using MobileStore.Controllers.Base;
using MobileStore.ViewModels;
using MobileStore.Models;

namespace MobileStore.Controllers
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
            var productView = new ProductViewModel();

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