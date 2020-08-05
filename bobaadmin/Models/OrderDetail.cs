using System;
using System.Collections.Generic;

namespace bobaadmin.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public List<OrderDetailItems> items {get;set;}

    }
    public class OrderDetailItems
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public int Quantity { get; set; }
        
    }
}
