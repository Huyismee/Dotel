
using EXE_Dotel.Models;

namespace EXE_Dotel.Repository.Rental
{
    public class RentalRepostiory : IRentalRepository
    {
        DotelDBContext dBContext;
        public RentalRepostiory(DotelDBContext context) { dBContext = context; }
        public Models.Rental GetRental(int id)
        {
            EXE_Dotel.Models.Rental? currentRental= dBContext.Rentals.FirstOrDefault(rental=> rental.RentalId == id);
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
    }
}
