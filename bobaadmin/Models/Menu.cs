﻿using System;
using System.Collections.Generic;

namespace bobaadmin.Models
{
    public partial class Menu
    {
        public string name { get; set; }
        public string description { get; set; }
        public string images { get; set; }
        public double price { get; set; }
        public bool active { get; set; }
        public bool ishot { get; set; }
        public bool issushi { get; set; }
        public bool ismilktea { get; set; }
    }
}