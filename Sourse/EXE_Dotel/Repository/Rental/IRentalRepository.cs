using EXE_Dotel.Models;
namespace EXE_Dotel.Repository.Rental
{
    public interface IRentalRepository
    {
        public EXE_Dotel.Models.Rental GetRental(int id);

        public List<EXE_Dotel.Models.Rental> GetRentals();

        //public List<EXE_Dotel.Models.Rental> GetRentalByFilters();

        public List<EXE_Dotel.Models.Rental> getRentalWithImage();


        public List<Models.Rental> getRentersPaging(int page, int pageSize);

        public int getListRentalsCount();

        public Models.Rental getRentalWithListImages(int rentalId);
    }
}
