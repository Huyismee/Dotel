using System;
using System.Collections.Generic;

namespace Dotel2.Models
{
    public partial class Rental
    {
        public Rental()
        {
            RentalListImages = new HashSet<RentalListImage>();
            RentalVideos = new HashSet<RentalVideo>();
            SponsorRentals = new HashSet<SponsorRental>();
        }

        public int RentalId { get; set; }
        public string RentalTitle { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? RoomArea { get; set; }
        public int? MaxPeople { get; set; }
        public string? ContactPhoneNumber { get; set; }
        public int? UserId { get; set; }
        public int? ViewNumber { get; set; }
        public bool? Bathroom { get; set; }
        public bool? Kitchen { get; set; }
        public int? BedroomNumber { get; set; }
        public string? Location { get; set; }
        public string? GoogleMap { get; set; }
        public bool Approval { get; set; }
        public bool? Status { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<RentalListImage> RentalListImages { get; set; }
        public virtual ICollection<RentalVideo> RentalVideos { get; set; }
        public virtual ICollection<SponsorRental> SponsorRentals { get; set; }
    }
}
