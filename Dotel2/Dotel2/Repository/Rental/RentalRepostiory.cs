
using Dotel2.Models;
using Dotel2.Repository.Rental;
using Microsoft.EntityFrameworkCore;

namespace EXE_Dotel.Repository.Rental
{
    public class RentalRepostiory : IRentalRepository
    {
        DotelDBContext dBContext;
        public RentalRepostiory(DotelDBContext context) { dBContext = context; }

        public int getListRentalsCount()
        {
            return dBContext.Rentals.Count();
        }

        public Dotel2.Models.Rental GetRental(int rentalId)
        {
            Console.WriteLine($"Fetching rental with ID: {rentalId}");

            var rental = dBContext.Rentals
                                 .Include(r => r.RentalListImages)
                                 .FirstOrDefault(r => r.RentalId == rentalId);

            if (rental == null)
            {
                // Debugging: Log if rental not found
                Console.WriteLine($"No rental found for ID: {rentalId}");
            }

            return rental;
        }

        public List<Dotel2.Models.Rental> GetRentals()
        {
            return dBContext.Rentals.ToList();
        }

        public List<Dotel2.Models.Rental> getRentalWithImage()
        {

            return dBContext.Rentals
            .Include(r => r.RentalListImages).ToList();
            ;
        }

        public Dotel2.Models.Rental getRentalWithListImages(int rentalId)
        {
            return dBContext.Rentals.Include(r => r.RentalListImages).FirstOrDefault(rental => rental.RentalId == rentalId);
        }

        public Dotel2.Models.Rental getRentalWithListImagesAndVideo(int rentalId)
        {
            return dBContext.Rentals.Include(r => r.RentalListImages)
                                    .Include(rental => rental.RentalVideos).AsSplitQuery()
                                    .FirstOrDefault(rental=> rental.RentalId==rentalId);
        }

        public List<Dotel2.Models.Rental> getRentersPaging(int page, int pageSize)
        {
            return dBContext.Rentals.Include(rental => rental.RentalListImages)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).OrderBy(re=>re.Price)
                .ToList();
        }

        public void getViewCountIncrease(Dotel2.Models.Rental rental)
        {
            if(rental == null) return;
            else
            {
                rental.ViewNumber += 1;
                dBContext.SaveChanges();
            }
        }
    }
}
