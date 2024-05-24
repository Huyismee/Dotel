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
        public string MainPhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string? SecondaryPhoneNumber { get; set; }
        public bool Status { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
