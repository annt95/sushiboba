using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FrontEnd.Models
{
    public class Cart
    {
        public Menu Product { get; set; }
        public int Quantity { get; set; }
    }


}
