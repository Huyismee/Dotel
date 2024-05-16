
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

        public Models.Rental GetRental(int id)
        {
            EXE_Dotel.Models.Rental? currentRental = dBContext.Rentals.Include(rental=> rental.RentalListImages)
                                                                      .FirstOrDefault(rental=> rental.RentalId== id);
            if (currentRental != null)
            {
                return currentRental;
            }
            return currentRental;
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

        public List<Models.Rental> getRentersPaging(int page, int pageSize)
        {
            return dBContext.Rentals.Include(rental => rental.RentalListImages)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
