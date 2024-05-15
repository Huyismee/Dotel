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

        public List<Rental>? rentals { get; private set; }
        public void OnGet()
        {
            rentals= rentalRepository.GetRentals();
        }
    }
}
