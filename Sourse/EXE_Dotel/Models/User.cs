﻿using System;
using System.Collections.Generic;

namespace EXE_Dotel.Models
{
    public partial class User
    {
        public User()
        {
            Rentals = new HashSet<Rental>();
        }

        public string MainPhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string? SecondaryPhoneNumber { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
