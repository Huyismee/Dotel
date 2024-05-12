using System;
using System.Collections.Generic;

namespace EXE_Dotel.Models
{
    public partial class Rental
    {
        public Rental()
        {
            RentalListImages = new HashSet<RentalListImage>();
            RentalVideos = new HashSet<RentalVideo>();
        }

        public int RentalId { get; set; }
        public decimal Price { get; set; }
        public decimal RoomArea { get; set; }
        public int MaxPeople { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<RentalListImage> RentalListImages { get; set; }
        public virtual ICollection<RentalVideo> RentalVideos { get; set; }
    }
}
