using Madelia.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Madelia.Controllers
{
    public class ProductController : Controller
    {
        private MadeliaDBContext _ctx;

        public ProductController(MadeliaDBContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
           try
            {
                // Get the product by ID
                Product product = _ctx.Products.Find(id);

                // Check if the product exists
                if (product == null)
                {
                    return NotFound(); // Return a 404 page
                }
                return View(product);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }

        }

    }
}
