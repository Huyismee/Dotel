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
        
        [BindProperty]
        public bool ApprovedOnly {  get; set; }

        public List<Rental> Rentals { get; set; }
        public void OnGet()
        {
            Rentals = _rentalRepository.GetRentals();
        }
        public void OnPost()
        {
            Rentals = _rentalRepository.GetRentals();
            if (ApprovedOnly)
            Rentals = Rentals.Where(r => r.Approval).ToList();
        }
    }
}
