using EXE_Dotel.Models;

namespace EXE_Dotel.Repository.RentalImages
{
    public class RentalImageRepository : iRentalImageRepository


    {

        private readonly DotelDBContext _dbContext;

        public RentalImageRepository(DotelDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<RentalListImage> GetListImageByRentalId(int rentalId)
        {

            List<RentalListImage> list= _dbContext.RentalListImages.Where(image => image.RentalId == rentalId).ToList();
            if (list == null)
            {
                Console.WriteLine("null");
            }
            return list;   
        }
    }
}
