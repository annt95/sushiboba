using System;
using System.Collections.Generic;

namespace FrontEnd.Models
{
    public partial class CartItems
    {
        public int CartItemsId { get; set; }
        public Menu menu { get; set; }
        public int amount { get; set; }
        public string ShoppingCartID { get; set; }
        
    }
}
