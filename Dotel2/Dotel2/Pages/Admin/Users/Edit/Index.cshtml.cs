using Dotel2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dotel2.Pages.Admin.Users.Edit
{
    public class IndexModel : PageModel
    {
        private readonly DotelDBContext _context;
        public IndexModel(DotelDBContext context)
        {
            _context = context;
        }
        public User User { get; set; }
        public void OnGet(int? id)
        {
            if (id == null) NotFound();
            User = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (User == null) NotFound();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            _context.Attach(User).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex) { }
            return RedirectToPage("./Index");
        }
    }
}
