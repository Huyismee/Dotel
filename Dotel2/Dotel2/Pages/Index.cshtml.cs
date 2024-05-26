using Dotel2.Models;
using Dotel2.Repository.Rental;
using EXE_Dotel.Repository.Rental;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dotel2.Models;
using Dotel2.Repository.Rental;
namespace Dotel2.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private readonly IRentalRepository rentalRepository;
        public IndexModel(IRentalRepository repository)
        {
            rentalRepository = repository;
        }
        public List<Rental>? rentals { get; private set; }
        public Dictionary<int, List<RentalListImage>> images { get; private set; }
        public void OnGet()
        {
            rentals = rentalRepository.GetRentals();
            images = new Dictionary<int, List<RentalListImage>>();
            foreach (var r in rentals)
            {
                var curListImg = rentalRepository.getRentalWithListImages(r.RentalId);
                //images[r.RentalId] = curListImg;

            }
        }
    }
}