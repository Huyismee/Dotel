using EXE_Dotel.Models;
using EXE_Dotel.Repository.Rental;
using EXE_Dotel.Repository.RentalImages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;

namespace EXE_Dotel.Pages
{
    public class property_detailsModel : PageModel
    {

        private readonly IRentalRepository rentalRepository;

        private readonly iRentalImageRepository imageRepository;

        public property_detailsModel(IRentalRepository rentalRepository, iRentalImageRepository imageRepository)
        {
            this.rentalRepository = rentalRepository;
            this.imageRepository = imageRepository;
        }

        [BindProperty (SupportsGet =true)] 
        
        public int PropertyId { get; set; }

        public Rental currRental { get; private set; }

        public List<RentalListImage> ?Images { get; private set; }

        public List<Rental> rentals { get; private set; }


        public Rental Rental { get; private set; }
        public void OnGet()

        {
            Console.WriteLine($"Current Page" +
                $": {PropertyId}");

            rentals= rentalRepository.GetRentals();

            currRental = rentalRepository.GetRental(PropertyId);

            Rental = rentalRepository.getRentalWithListImagesAndVideo(PropertyId);
            foreach (Rental rentla in rentals)
            {
                Console.WriteLine(rentla);
                
                Console.WriteLine(currRental);

                Console.WriteLine(Rental.RentalVideos.Count);
            }
            
            if (PropertyId < 0)
            {
                //handle error => send message
                return ;
            }
            
            Images = imageRepository.GetListImageByRentalId(PropertyId);

            Console.WriteLine(currRental);
            if (currRental == null)
            {
                //handle error => semd message or send to a new page
                Console.WriteLine("a");

                return;
            }

            
        }
    }
}
