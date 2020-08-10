using System;
using System.Collections.Generic;

namespace FrontEnd.Models
{
    public partial class OrderView
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderTime { get; set; }
        public string CustomerPhone { get; set; }
        public string TotalMoney { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public string ShippingType { get; set; }
        public int StatusID { get; set; }

    }
}
