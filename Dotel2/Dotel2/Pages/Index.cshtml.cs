using Dotel2.Models;
using Dotel2.Repository.Rental;
using EXE_Dotel.Repository.Rental;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dotel2.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private readonly IRentalRepository rentalRepository;
        private readonly DotelDBContext _context;
        public IndexModel(IRentalRepository repository, DotelDBContext context)
        {
            rentalRepository = repository;
            _context = context;
        }
        public bool IsLoggedIn { get; private set; }
        public List<Rental> rentals { get; private set; }
        public Dictionary<int, List<RentalListImage>> images { get; private set; }


        public string? SessionValue { get; private set; }


        [BindProperty(SupportsGet = true)]
        public string Location { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Type { get; set; }

        [BindProperty(SupportsGet = true)]
        public string AreaRange { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PriceRange { get; set; }

        public List<Rental> FilteredRenter { get; set; }
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
            FilteredRenter = rentalRepository.getFilteredRental(Location, Type, AreaRange, PriceRange);
           
            if (TempData.ContainsKey("FilteredRentals"))
            {
                var rentalsJson = TempData["FilteredRentals"].ToString();
                FilteredRenter = JsonConvert.DeserializeObject<List<Rental>>(rentalsJson);
                rentals = FilteredRenter; // Set Rentals to FilteredRenter if available
            }

            
            
            foreach (var r in rentals)
            {
                SessionValue = HttpContext.Session.GetString("UserSession");
                var curListImg = rentalRepository.getRentalWithListImages(r.RentalId);
                //images[r.RentalId] = curListImg;

            }
            ViewData["CntPost"] = rentals.Count;
            ViewData["CntUser"] = _context.Users.ToList().Count;
        }
        public IActionResult OnPostIncrementViewCount(int rentalId,int userid)
        {
            var rental = rentalRepository.GetRental(rentalId);

            if (rental != null)
            {
                rentalRepository.getViewCountIncrease(rental);
                TempData["UserId"] = userid;
                return RedirectToPage("RentHomeDetails", new { id = rentalId });
            }
            return NotFound();
        }


        public IActionResult OnPostIndex()
        {
            Console.WriteLine(Location);
            Console.WriteLine(Type);
            FilteredRenter = rentalRepository.getFilteredRental(Location, Type, AreaRange, PriceRange);
            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            
            TempData["FilteredRentals"] = JsonConvert.SerializeObject(FilteredRenter, jsonSettings);
            return RedirectToPage(new {Location, Type, AreaRange, PriceRange});
        }
    }


}
