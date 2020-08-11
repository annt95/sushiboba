using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FrontEnd.Models
{
    public class Cart
    {
        //private readonly bobachaContext _bobachaContext;
        //private Cart (bobachaContext bobachaContext)
        //{
        //    _bobachaContext = bobachaContext;
        //}
        public string ShoppingCartID { get; set; }
        public List<Cart> cart { get; set; }
        public static Cart GetCart(IServiceProvider provider)
        {
            ISession ss = provider.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            var context = provider.GetService<bobachaContext>();
            string cartID = ss.GetString("CartID") ?? Guid.NewGuid().ToString();
            ss.SetString("CartID", cartID);

            return new Cart(context) { ShoppingCartID = cartID };
        }
        public void AddtoCart(Menu menu, int amount)
        {
            
        }
    }
    
}
