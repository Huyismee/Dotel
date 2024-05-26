using Dotel2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Dotel2.Pages.Admin.Users
{
    public class UserModel : PageModel
    {
        private readonly DotelDBContext _context;
        public UserModel(DotelDBContext context)
        {
            _context = context;
        }
        public List<User> Users { get; set; }
        public void OnGet()
        { 
            Users = _context.Users.Include(r => r.Role).ToList();  
        }
    }
}
