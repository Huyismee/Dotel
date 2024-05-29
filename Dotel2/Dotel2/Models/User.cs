using System;
using System.Collections.Generic;

namespace Dotel2.Models
{
    public partial class User
    {
        public User()
        {
            Rentals = new HashSet<Rental>();
        }

        public int UserId { get; set; }
        public string MainPhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string SecondaryPhoneNumber { get; set; }
        public bool Status { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
