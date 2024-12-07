using Madelia.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Madelia.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var cart = HttpContext.Session.GetCart(); // Use your session extension method
            ViewBag.CartCount = cart.Sum(c => c.Quantity);
        }
    }
}
