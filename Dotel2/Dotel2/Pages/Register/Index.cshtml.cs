using Dotel2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace Dotel2.Pages.Register
{
    public class IndexModel : PageModel
    {
        private readonly DotelDBContext _context;
        public IndexModel(DotelDBContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            var emailExist = _context.Users.FirstOrDefault(s => s.Email.Equals(Request.Form["Email"]));
            if (emailExist != null)
            {
                TempData["ErrorMessage"] = "Email exist.";
                return Page();
            }

            if (!Request.Form["Password"].Equals(Request.Form["RepeatPassword"]))
            {
                TempData["ErrorMessage"] = "Passwords do not match.";
                return Page();
            }
            var hashedPassword = GetHashedPassword(Request.Form["Password"]);
            var newUser = new User
            {
                Fullname = Request.Form["FullName"],
                Email = Request.Form["Email"],
                Password = hashedPassword,
                MainPhoneNumber = Request.Form["Phone"],
                RoleId = 2,// admin = 1, guest = 2, đã có role trong db mới add được
                Status = true,
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Register successfully.";
            return Page();
        }

        private string GetHashedPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
