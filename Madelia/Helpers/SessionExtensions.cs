using Madelia.Models;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace Madelia.Helpers
{
    public static class SessionExtensions
    {
        private const string CartKey = "Cart";

        public static void SetCart(this ISession session, List<CartItem> cart)
        {
            session.SetString(CartKey, JsonConvert.SerializeObject(cart));
        }

        public static List<CartItem> GetCart(this ISession session)
        {
            var cartJson = session.GetString(CartKey);
            return string.IsNullOrEmpty(cartJson) ? new List<CartItem>() : JsonConvert.DeserializeObject<List<CartItem>>(cartJson);
        }
    }
}
