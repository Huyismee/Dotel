using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Dotel2.Models;

namespace Dotel2.Pages.FormRentHome
{
    public class IndexModel : PageModel
    {
        private readonly DotelDBContext _context;
        private readonly string _uploadDirectory = "wwwroot/uploads";

        public IndexModel(DotelDBContext context)
        {
            _context = context;

            if (!Directory.Exists(_uploadDirectory))
            {
                Directory.CreateDirectory(_uploadDirectory);
            }
        }

        [BindProperty] public string Title { get; set; }
        [BindProperty] public decimal Price { get; set; }
        [BindProperty] public decimal Area { get; set; }
        [BindProperty] public string Address { get; set; }
        [BindProperty] public string Description { get; set; }
        [BindProperty] public List<IFormFile> MediaFiles { get; set; }

        public ActionResult OnGet() {
            var userSession = HttpContext.Session.GetString("UserSession");
            if (string.IsNullOrEmpty(userSession))
            {
                return RedirectToPage("/Login/index");
            }
            return Page();
        }

        public ActionResult OnPost()
        {
            var userSession = HttpContext.Session.GetString("UserSession");
            var user = _context.Users.FirstOrDefault(s => s.Email == userSession);
            if (user == null)
            {
                return RedirectToPage("/login");
            }

            var rental = new Rental
            {
                UserId = user.UserId,
                RentalTitle = Title,
                Price = Price,
                RoomArea = Area,
                Description = Description,
                ContactPhoneNumber = user.MainPhoneNumber,
                Location = Address,
                Status = true,
                Approval = false
            };

            _context.Rentals.Add(rental);
            _context.SaveChanges();

            var imagePaths = new List<string>(); // Danh sách đường dẫn ảnh
            var videoPaths = new List<string>(); // Danh sách đường dẫn video

            foreach (var file in MediaFiles)
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(_uploadDirectory, Path.GetFileName(file.FileName));
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    if (file.ContentType.StartsWith("image/"))
                    {
                        imagePaths.Add(Path.Combine("uploads", file.FileName)); // Thêm đường dẫn ảnh vào danh sách
                    }
                    else if (file.ContentType.StartsWith("video/"))
                    {
                        videoPaths.Add(Path.Combine("uploads", file.FileName)); // Thêm đường dẫn video vào danh sách
                    }
                }
            }

            // Lưu đường dẫn ảnh và video vào cơ sở dữ liệu
            foreach (var imagePath in imagePaths)
            {
                rental.RentalListImages.Add(new RentalListImage { RentalId = rental.RentalId, Sourse = imagePath });
            }

            foreach (var videoPath in videoPaths)
            {
                rental.RentalVideos.Add(new RentalVideo { RentalId = rental.RentalId, Sourse = videoPath });
            }

            _context.SaveChanges();

            return RedirectToPage("/Index");
        }

    }
}
