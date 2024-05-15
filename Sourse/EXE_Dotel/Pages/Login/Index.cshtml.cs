using EXE_Dotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text;

namespace EXE_Dotel.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly DotelDBContext _context;
        public IndexModel(DotelDBContext context)
        {
            _context = context;
        }
        [BindProperty] public string username { get; set; }
        [BindProperty] public string password { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (LoginSuccessful())
            {
                return RedirectToPage("/Index");
            }
            else
            {
				TempData["ErrorMessage"] = "UserName or PassWord invalid.";
				return Page();
            }
        }

        public bool LoginSuccessful()
        {
			var hashedPassword = GetHashedPassword(password);
			var user = _context.Users.FirstOrDefault(s => s.Email.Equals(username) && s.Password.Equals(hashedPassword));
			if (user == null)
            {
				return false;
			}else
            {
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

	}
}
