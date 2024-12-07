using Madelia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Madelia.Helpers;

namespace Madelia.Controllers
{
    public class CartController : BaseController
    {
        private MadeliaDBContext _ctx;

        public CartController(MadeliaDBContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            // Get the cart from the session
            var cart = HttpContext.Session.GetCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult Add(int productId)
        {
            var product = _ctx.Products.Find(productId);
            if (product == null) return NotFound();

            // Get the current cart from the session
            var cart = HttpContext.Session.GetCart();

            // Check if the product is already in the cart
            var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);
            if (cartItem != null)
            {
                // Increase the quantity
                cartItem.Quantity++;
            }
            else
            {
                // Add new item to the cart
                cart.Add(new CartItem
                {
                    ProductId = product.ProductId,
                    Name = product.ProductName,
                    Price = product.Price,
                    Quantity = 1
                });
            }

            HttpContext.Session.SetCart(cart);


            // Redirect back to the current page or the cart page
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null) cart.Remove(item);
            HttpContext.Session.SetCart(cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateCart(Dictionary<int, int> Quantities)
        {
            // Get the cart from the session
            var cart = HttpContext.Session.GetCart();

            // Update quantities based on the provided data
            foreach (var keyValue in Quantities)
            {
                var productId = keyValue.Key;
                var quantity = keyValue.Value;

                // Find the cart item and update its quantity
                var cartItem = cart.FirstOrDefault(c => c.ProductId == productId);
                if (cartItem != null && quantity > 0)
                {
                    cartItem.Quantity = quantity;
                }
            }

            // Save the updated cart back to the session
            HttpContext.Session.SetCart(cart);

            // Redirect back to the cart page
            return RedirectToAction("Index");
        }


    }
}
