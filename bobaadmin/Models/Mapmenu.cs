using System;
using System.Collections.Generic;

namespace bobaadmin.Models
{
    public partial class Mapmenu
    {
        public int Id { get; set; }
        public int? Cateid { get; set; }
        public int? Menuid { get; set; }
    }
}
