using System;
using System.Collections.Generic;

namespace Dotel2.Models
{
    public partial class RentalListImage
    {
        public int ImageId { get; set; }
        public int RentalId { get; set; }
        public string Sourse { get; set; }

        public virtual Rental Rental { get; set; }
    }
}
