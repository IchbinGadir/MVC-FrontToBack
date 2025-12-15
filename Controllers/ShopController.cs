using Microsoft.AspNetCore.Mvc;

namespace ProniaA.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult wishlist()
        {
            return View();
        }
        public IActionResult SinglePage()
        {
            return View();
        }
    }
}
