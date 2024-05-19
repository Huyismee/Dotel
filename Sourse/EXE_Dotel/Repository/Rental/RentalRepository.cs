
using EXE_Dotel.Models;
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

        public Models.Rental GetRental(int rentalId)
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

        public List<Models.Rental> GetRentals()
        {
            return dBContext.Rentals.ToList();
        }

        public List<Models.Rental> getRentalWithImage()
        {

            return dBContext.Rentals
            .Include(r => r.RentalListImages).ToList();
            ;
        }

        public Models.Rental getRentalWithListImages(int rentalId)
        {
            return dBContext.Rentals.Include(r => r.RentalListImages).FirstOrDefault(rental => rental.RentalId == rentalId);
        }

        public List<Models.Rental> getRentersPaging(int page, int pageSize)
        {
            return dBContext.Rentals.Include(rental => rental.RentalListImages)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
