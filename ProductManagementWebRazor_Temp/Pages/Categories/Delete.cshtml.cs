using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductManagementWebRazor_Temp.Data;
using ProductManagementWebRazor_Temp.Models;

namespace ProductManagementWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            if (id != 0)
            {
                category = _db.Categories.FirstOrDefault(c => c.Id == id)!;
            }
        }

        public IActionResult OnPost()
        {
            Category? obj = _db.Categories.Find(category.Id)!;
            if (obj == null) return NotFound();
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["message"] = "Category deleted successfully";
            TempData["status"] = "danger";
            return RedirectToPage("Index");
        }
    }
}
