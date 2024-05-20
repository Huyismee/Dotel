using EXE_Dotel.Models;
using EXE_Dotel.Repository.Rental;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EXE_Dotel.Pages
{
    public class propertyModel : PageModel
    {
        private readonly IRentalRepository rentalRepository;
        public propertyModel(IRentalRepository repository)
        {
            rentalRepository = repository;
        }

       

        public int Total { get ; private set; }

        public int PageSize { get; private set; } = 3;

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; private set; } 
        public List<Rental>? rentals { get; private set; } 
        public void OnGet()
        {

            Console.WriteLine($"Current Page: {CurrentPage}");
            Total = rentalRepository.getListRentalsCount();
            TotalPages= (int)Math.Ceiling(Total / (double)PageSize);
            rentals = rentalRepository.getRentersPaging(CurrentPage, PageSize);
        }
    }
}
