using EXE_Dotel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EXE_Dotel.Pages.RequestCode
{
    public class IndexModel : PageModel
    {
        private readonly DotelDBContext _context;
        public IndexModel(DotelDBContext context)
        {
            _context = context;
        }
        [BindProperty] public string Email { get; set; }
        public IActionResult OnGet()
        {
            var emailSession = HttpContext.Session.GetString("EmailSession");
            if (string.IsNullOrEmpty(emailSession))
            {
                return RedirectToPage("/forgotpassword/index");
            }
            else
            {
                Email = emailSession;
                return Page();
            }
        }
    }
}
