using Dotel2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dotel2.Pages
{
    public class AdminModel : PageModel
    {
        private readonly DotelDBContext _context;
        public AdminModel(DotelDBContext context)
        {
            _context = context;
        }
        public List<User> Users { get; set; }
        public void OnGet()
        {
            Users = _context.Users.ToList();
        }
    }
}
