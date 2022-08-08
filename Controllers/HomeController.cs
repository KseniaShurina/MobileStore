using Microsoft.AspNetCore.Mvc;
using MobileStore.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MobileStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly MobileContext _context;

        public HomeController(MobileContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var phones = await _context.Phones.ToListAsync();
            return View(phones);
        }

        //[Route("Buy/{id}")]
        public async Task<IActionResult> Buy(int id)
        {
            var phone = await _context.Phones.Where(i => i.Id == id).FirstOrDefaultAsync();
            return View(phone);
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
