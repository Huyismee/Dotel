﻿using System;
using System.Collections.Generic;

namespace Dotel2.Models
{
    public partial class Sponsorship
    {
        public Sponsorship()
        {
            SponsorRentals = new HashSet<SponsorRental>();
        }

        public int SponsorId { get; set; }
        public string SponsorName { get; set; }
        public string SponsorDes { get; set; }

        public virtual ICollection<SponsorRental> SponsorRentals { get; set; }
    }
}
