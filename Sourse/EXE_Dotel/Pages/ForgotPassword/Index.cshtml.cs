using EXE_Dotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EXE_Dotel.Pages.Forgot
{
    public class IndexModel : PageModel
    {
        private readonly DotelDBContext _context;
        public IndexModel(DotelDBContext context)
        {
            _context = context;
        }
        [BindProperty] public string username { get; set; }
        public void OnGet()
        {
            var emailSession = HttpContext.Session.GetString("EmailSession");
            if (!string.IsNullOrEmpty(emailSession))
            {
                HttpContext.Session.Remove("EmailSession");
            }
        }
        public IActionResult OnPost() {
            var user = _context.Users.FirstOrDefault(s => s.Email.Equals(username));
            if (user == null)
            {
                TempData["ErrorMessage"] = "Email not exist.";
                return Page();
            }
            else
            {
                HttpContext.Session.SetString("EmailSession", user.Email);
                return RedirectToPage("/requestcode/index");
            }
        }
    }
}
