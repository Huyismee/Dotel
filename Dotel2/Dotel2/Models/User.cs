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
        public string? MainPhoneNumber { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string? SecondaryPhoneNumber { get; set; }
        public bool Status { get; set; }
        public int RoleId { get; set; }
        public bool? CheckEmail { get; set; }
        public bool? CheckPhone { get; set; }
        public string? EmailVerificationCode { get; set; }
        public DateTime? EmailVerificationCodeExpires { get; set; }
        public string? PhoneVerificationCode { get; set; }
        public DateTime? PhoneVerificationCodeExpires { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
