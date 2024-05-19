using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EXE_Dotel.Models;
using EXE_Dotel.Repository.Rental;
namespace EXE_Dotel.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IRentalRepository rentalRepository;
        public IndexModel(IRentalRepository repository)
        {
            rentalRepository = repository;
        }
        public List<Rental> ? rentals {  get; private set; }
        public void OnGet()
        {
            rentals = rentalRepository.GetRentals(); ;
        }
    }
}
