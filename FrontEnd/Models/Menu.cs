using System;
using System.Collections.Generic;

namespace FrontEnd.Models
{
    public partial class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Images { get; set; }
        public string Price { get; set; }
        public bool? Active { get; set; }
        public bool? Ishot { get; set; }
        public bool? Issushi { get; set; }
        public bool? Ismilktea { get; set; }
        public bool? Isdelete { get; set; }
    }
}
