using System;
using System.Collections.Generic;

namespace EXE_Dotel.Models
{
    public partial class RentalVideo
    {
        public int VideoId { get; set; }
        public string Sourse { get; set; } = null!;
        public int RentalId { get; set; }

        public virtual Rental Rental { get; set; } = null!;
    }
}
