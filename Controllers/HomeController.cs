using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MobileStore.Contexts;

namespace MobileStore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly DefaultContext _context;

        public HomeController(DefaultContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var phones = await _context.Phones.ToListAsync();
            return View(phones);
        }

        public async Task<IActionResult> Buy(int id)
        {
            var phone = await _context.Phones.Where(i => i.Id == id).FirstOrDefaultAsync();
            return View(phone);
        }

        //[HttpPost]
        //public string Buy(Order order)
        //{
        //    _context.Orders.Add(order);
        //    // сохраняем в бд все изменения
        //    _context.SaveChanges();
        //    return $"Спасибо, {order.Name}, за покупку!";
        //}

        //public async Task<IActionResult> Registration(int? id)
        //{
        //    //var item = id;
        //    var phone = await _context.Phones.Where(i => i.Id == id).FirstOrDefaultAsync();
        //    return View(phone);
        //}
    }

    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //public IActionResult Error()
    //{
    //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //}
}
