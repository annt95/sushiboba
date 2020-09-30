using System;
using System.Collections.Generic;

namespace FrontEnd.Models
{
    public partial class OrderItem
    {
        public string distance { get; set; }
        public string fname { get; set; }
        public string fphone { get; set; }
        public string location { get; set; }
        public string note { get; set; }
        public string optdeli { get; set; }
    }
    public class OrderDetailItems
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public int Quantity { get; set; }

    }
}
