using Dotel2.Models;
using Dotel2.Repository.Rental;
using EXE_Dotel.Repository.Rental;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Dotel2.Pages
{
    public class RentHomeModel : PageModel
    {
        private readonly IRentalRepository repository;

        public RentHomeModel(IRentalRepository repo)
        {
            repository = repo;

        }
        public List<Rental> Rentals { get; set; }
        public int Total { get; private set; }

        public int PageSize { get; private set; } = 3;

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; private set; }

        [BindProperty(SupportsGet =true)]

        public string Location { get; set; }

        [BindProperty(SupportsGet =true)]

        public string Type { get; set; }

        [BindProperty(SupportsGet = true)]
        public string AreaRange { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PriceRange { get; set; }
        public List<Rental> FilteredRenter { get; private set; }

        public void OnGet()
        {
            Console.WriteLine($"Current Page: {CurrentPage}");

            Total = repository.getListRentalsCount();
            TotalPages = (int)Math.Ceiling(Total / (double)PageSize);
            Rentals = repository.getRentersPaging(CurrentPage, PageSize);
            FilteredRenter= repository.getFilterRentalPaging(Location,Type, AreaRange, PriceRange, CurrentPage, PageSize);

            foreach (var rental in Rentals)
            {
                Console.WriteLine(rental);
            }
            if (TempData.ContainsKey("FilteredRentals"))
            {
                var rentalsJson = TempData["FilteredRentals"].ToString();
                FilteredRenter = JsonConvert.DeserializeObject<List<Rental>>(rentalsJson);
                Rentals = FilteredRenter; // Set Rentals to FilteredRenter if available
            }
        }

        public IActionResult OnPostIncrementViewCount(int rentalId)
        {
            var rental = repository.GetRental(rentalId);
            if (rental != null)
            {
                repository.getViewCountIncrease(rental);
                return RedirectToPage("RentHomeDetails", new { id = rentalId });
            }
            return NotFound();
        }
        public IActionResult OnPostIndex()
        {
            Console.WriteLine(Location);
            Console.WriteLine(Type);
            FilteredRenter = repository.getFilterRentalPaging(Location,Type,AreaRange,PriceRange,CurrentPage,PageSize);
            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            TempData["FilteredRentals"] = JsonConvert.SerializeObject(FilteredRenter, jsonSettings);
            return RedirectToPage(new { Location, Type, AreaRange, PriceRange });
        }
    }
}
