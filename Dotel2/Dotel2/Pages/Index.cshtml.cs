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
        public bool IsLoggedIn { get; private set; }
        public List<Rental>? rentals { get; private set; }
        public Dictionary<int, List<RentalListImage>> images { get; private set; }
        public void OnGet()
        {
            var userSession = HttpContext.Session.GetString("UserSession");
            IsLoggedIn = !string.IsNullOrEmpty(userSession);

            rentals = rentalRepository.getRentalWithImage();
            images = new Dictionary<int, List<RentalListImage>>();
            foreach (var r in rentals)
            {
                var curListImg = rentalRepository.getRentalWithListImages(r.RentalId);
                //images[r.RentalId] = curListImg;

            }
        }
    }
}