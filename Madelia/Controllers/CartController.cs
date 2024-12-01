using Microsoft.AspNetCore.Mvc;

namespace Madelia.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(int productId)
        {
            // Simulate adding the product to the cart
            if (HttpContext.Session.GetInt32("CartCount") == null)
            {
                HttpContext.Session.SetInt32("CartCount", 1);
            }
            else
            {
                int cartCount = HttpContext.Session.GetInt32("CartCount").Value;
                HttpContext.Session.SetInt32("CartCount", cartCount + 1);
            }

            // Redirect back to the current page or the cart page
            return RedirectToAction("Index", "Home");
        }

    }
}
