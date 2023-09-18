using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductManagementWebRazor_Temp.Data;
using ProductManagementWebRazor_Temp.Models;

namespace ProductManagementWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category category { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            if (id != 0)
            {
                category = _db.Categories.Find(id)!;
            }
        }

        public IActionResult OnPost()
        {
            if (category == null)
                return Page();
            if (category.Name != null)
            {
                // Validate if the category name already exists
                if (_db.Categories.Any(x => x.Name!.ToLower().Equals(category.Name!.ToLower())))
                    ModelState.AddModelError("", "This category already exists");
                if (category.Name!.ToLower().Equals("test") || category.Name!.ToLower().Equals("tests"))
                    ModelState.AddModelError("", $"{category.Name} is not a valid name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["message"] = "Category updated successfully";
                TempData["status"] = "warning";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
