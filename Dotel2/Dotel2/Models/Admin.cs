using System;
using System.Collections.Generic;

namespace Dotel2.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}
