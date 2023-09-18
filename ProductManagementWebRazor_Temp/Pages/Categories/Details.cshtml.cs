using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductManagementWebRazor_Temp.Data;
using ProductManagementWebRazor_Temp.Models;

namespace ProductManagementWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Category categories { get; set; }
        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult OnGet(int id)
        {
            if (id != 0)
            {
                categories = _context.Categories.Find(id)!;
                return Page();
            }
            return RedirectToPage("Index");
        }
    }
}
