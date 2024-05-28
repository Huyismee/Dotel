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

        public string ? SessionValue { get; private set; }

        //Thanh
        public string? userSessionTime { get; set; }
        //
        public void OnGet()
        {
            var userSession = HttpContext.Session.GetString("UserSession");
            //Thanh
            userSessionTime = userSession;
            //

            IsLoggedIn = !string.IsNullOrEmpty(userSession);

            rentals = rentalRepository.getRentalWithImage();
            images = new Dictionary<int, List<RentalListImage>>();
            foreach (var r in rentals)
            {
                SessionValue = HttpContext.Session.GetString("UserSession");
                var curListImg = rentalRepository.getRentalWithListImages(r.RentalId);
                //images[r.RentalId] = curListImg;

            }
        }
        public IActionResult OnPostIncrementViewCount(int rentalId)
        {
            var rental = rentalRepository.GetRental(rentalId);
            if (rental != null)
            {
                rentalRepository.getViewCountIncrease(rental);
                return RedirectToPage("RentHomeDetails", new { id = rentalId });
            }
            return NotFound();
        }
    }
}