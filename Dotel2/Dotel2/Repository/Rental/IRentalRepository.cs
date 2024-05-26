


using Dotel2.Models;

namespace Dotel2.Repository.Rental
{
    public interface IRentalRepository
    {
        public Dotel2.Models.Rental GetRental(int id);

        public List<Dotel2.Models.Rental> GetRentals();

        //public List<EXE_Dotel.Models.Rental> GetRentalByFilters();

        public List<Dotel2.Models.Rental> getRentalWithImage();


        public List<Models.Rental> getRentersPaging(int  page, int pageSize);

        public int getListRentalsCount();

        public Models.Rental getRentalWithListImages(int rentalId);


        public Models.Rental getRentalWithListImagesAndVideo(int rentalId);

        public void getViewCountIncrease(Models.Rental rental);
    }
}
