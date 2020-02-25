using System;
using System.Collections.Generic;

namespace admin.Models
{
    public partial class Admins
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public DateTime Created { get; set; }
    }
}
