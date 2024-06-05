using Dotel2.Models;
using Dotel2.Repository.Rental;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dotel2.Pages.Admin.Rentals
{
    public class RentalModel : PageModel
    {
        private readonly DotelDBContext _context;
        private readonly IRentalRepository _rentalRepository;
        public RentalModel (DotelDBContext context, IRentalRepository rentalRepository)
        {
            _context = context;
            _rentalRepository = rentalRepository;
        }
        public List <Rental> Rentals {  get; set; }

        public void OnGet()
        {
            Rentals = _rentalRepository.getApprovaledRentals();
        }
    }
}
