using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EXE_Dotel.Models;
using EXE_Dotel.Repository.Rental;
using EXE_Dotel.Repository.RentalImages;
namespace EXE_Dotel.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IRentalRepository rentalRepository;
        private readonly iRentalImageRepository rentalImageRepository;
        public IndexModel(IRentalRepository repository, iRentalImageRepository imageRepository)
        {
            rentalRepository = repository;
            rentalImageRepository = imageRepository;
        }
        public List<Rental> ? rentals {  get; private set; }
        public Dictionary  <int, List<RentalListImage>> images { get; private set; }
        public void OnGet()
        {
            rentals = rentalRepository.GetRentals();
            images = new Dictionary<int, List<RentalListImage>> ();
            foreach (var r in rentals)
            {
                var curListImg = rentalImageRepository.GetListImageByRentalId(r.RentalId);
                images[r.RentalId] = curListImg;
            }
        }
    }
}
