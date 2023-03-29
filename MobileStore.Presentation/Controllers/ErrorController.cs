using Microsoft.AspNetCore.Mvc;

namespace MobileStore.Presentation.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("/error")]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                // here is the trick
                this.HttpContext.Response.StatusCode = statusCode.Value;
            }

            //return a static file. 
            return View();

            // or return View 
            // return View(<view name based on statusCode>);
        }
    }
}
