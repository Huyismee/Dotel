using EXE_Dotel.Models;

namespace EXE_Dotel.Repository.RentalImages
{
    public interface iRentalImageRepository
    {
        public List<RentalListImage> GetListImageByRentalId(int rentalId);
    }
}
