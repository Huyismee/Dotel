using Dotel2.Models;
using Dotel2.Repository.Rental;
using EXE_Dotel.Repository.Rental;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dotel2.Pages
{
    public class IndexModel : PageModel
    {
        
        private readonly IRentalRepository repository;

        public IndexModel(IRentalRepository repo)
        {
            repository = repo;

        }
        public List<Rental> Rentals { get; set; }
        public int Total { get; private set; }

        public int PageSize { get; private set; } = 3;

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; private set; }

        public void OnGet()
        {
            Console.WriteLine($"Current Page: {CurrentPage}");

            Total = repository.getListRentalsCount();
            TotalPages = (int)Math.Ceiling(Total / (double)PageSize);
            Rentals = repository.getRentersPaging(CurrentPage, PageSize);
            foreach (var rental in Rentals)
            {
                Console.WriteLine(rental);
            }
        }
    }
}
