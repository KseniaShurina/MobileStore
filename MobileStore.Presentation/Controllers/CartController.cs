using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobileStore.Infrastructure.Contexts;
using MobileStore.Infrastructure.Models;
using MobileStore.Presentation.Controllers.Base;
using MobileStore.Presentation.ViewModels;

namespace MobileStore.Presentation.Controllers
{
    public class CartController : MvcControllerBaseSecure
    {
        private DefaultContext _context;
        public CartController(DefaultContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var cartViewModel = new CartViewModel();//для взаимодействия с вьюхой

            cartViewModel.CartItems = await _context.CartItems
                .AsNoTracking()
                .Include(i => i.Product)
                .Where(i => i.UserId == UserId)
                .ToListAsync();

            return View(cartViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int productId, [Range(1, int.MaxValue)] int quantity)
        {
            var current = await _context.CartItems.FirstOrDefaultAsync(i => i.ProductId == productId);

            if (current != null)
            {
                current.Quantity += quantity;
            }
            else
            {
                var newCartItem = new CartItem
                {
                    ProductId = productId,
                    UserId = UserId,
                    Quantity = quantity,
                };
                await _context.CartItems.AddAsync(newCartItem);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, [Range(1, int.MaxValue)] int quantity)
        {
            var item = await _context.CartItems.FirstOrDefaultAsync(x => x.Id == cartItemId);
            if (item != null)
            {
                item.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }

            return RedirectToAction("Index", "Cart");
        }

        public async Task<IActionResult> Remove(int cartItemId)
        {
            var item = await _context.CartItems.FirstOrDefaultAsync(i => i.Id == cartItemId);

            if (item == null)
            {
                return NotFound();
            }

            var removeItem = _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Cart");
        }
    }
}
