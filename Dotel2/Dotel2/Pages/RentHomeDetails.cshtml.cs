using Dotel2.Models;
using Dotel2.Repository.Rental;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dotel2.Pages
{
    public class RentHomeDetailsModel : PageModel
    {
        private readonly IRentalRepository repository;
        public RentHomeDetailsModel(IRentalRepository repo) { 
               repository = repo;
        }
        
        

        public Rental Rental { get; set; }
        public void OnGet(int Id)
        {
            Rental= repository.GetRental(Id);
            Console.WriteLine(Rental);
        }
    }
}
