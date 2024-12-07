using Madelia.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Madelia.Controllers
{
    public class HomeController : BaseController
    {
        private MadeliaDBContext _ctx;

        public HomeController(MadeliaDBContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index(string activeCategory = "all")
        {

            // Get all categories from the database
            List<Category> categories = _ctx.Categories.ToList();

            // Add the "all" option at the beginning of the categories list
            categories.Insert(0, new Category
            {
                CategoryId = "all",
                CategoryName = "all"
            });

            // Store the categories in ViewBag
            ViewBag.Categories = categories;

            // Start with all products
            IQueryable<Product> products = _ctx.Products;

            if (!string.Equals(activeCategory, "all", StringComparison.OrdinalIgnoreCase))
            {
                // Convert both values to lowercase for case-insensitive comparison
                string lowerActiveCategory = activeCategory.ToLower();

                products = from product in _ctx.Products
                           join pc in _ctx.ProductCategories on product.ProductId equals pc.ProductId
                           where pc.CategoryId.ToLower() == lowerActiveCategory
                           select product;
            }



            // Convert the filtered query to a list
            var filteredProducts = products.ToList();

            // Pass the filtered products to the view
            return View(filteredProducts);
        }


        public IActionResult Privacy()
        {
            return View();
        }

    }
}
