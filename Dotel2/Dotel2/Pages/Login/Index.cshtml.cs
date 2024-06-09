using Dotel2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Dotel2.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly DotelDBContext _context;

        public IndexModel(DotelDBContext context)
        {
            _context = context;
        }

        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Password { get; set; }

        public void OnGet()
        {
            string userJson = HttpContext.Session.GetString("userJson");
            if (!string.IsNullOrEmpty(userJson))
            {
                HttpContext.Session.Remove("userJson");
            }
        }

        public IActionResult OnPost()
        {
            if (LoginSuccessful())
            {
                return RedirectToPage("/Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Tài khoản hoặc mật khẩu không đúng.";
                return Page();
            }
        }

        public bool LoginSuccessful()
        {
            var hashedPassword = GetHashedPassword(Password);
            User user = null;

            if (IsValidEmail(Email))
            {
                user = _context.Users.FirstOrDefault(s => s.Email.Equals(Email) && s.Password.Equals(hashedPassword));
                if (user != null && user.CheckEmail != true)
                {
                    TempData["ErrorMessage"] = "Email chưa được xác thực.";
                    return false;
                }
            }
            else if (IsValidPhone(Email))
            {
                user = _context.Users.FirstOrDefault(s => s.MainPhoneNumber.Equals(Email) && s.Password.Equals(hashedPassword));
                if (user != null && user.CheckPhone != true)
                {
                    TempData["ErrorMessage"] = "Số điện thoại chưa được xác thực.";
                    return false;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Định dạng tài khoản không hợp lệ.";
                return false;
            }

            if (user == null)
            {
                return false;
            }
            else
            {
                if (!user.Status) // Deactivated
                {
                    TempData["ErrorMessage"] = "Tài khoản đã bị khóa.";
                    return false;
                }
                else if (user.RoleId != 2) // Not a guest
                {
                    TempData["ErrorMessage"] = "Truy cập bị từ chối.";
                    return false;
                }

                // Set session
                string userJson = JsonConvert.SerializeObject(user);
                HttpContext.Session.SetString("userJson", userJson);

                return true;
            }
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

        private bool IsValidEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }

        private bool IsValidPhone(string phone)
        {
            var phonePattern = @"^\d{10}$";
            return Regex.IsMatch(phone, phonePattern);
        }
    }
}
