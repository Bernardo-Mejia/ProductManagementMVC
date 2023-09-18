using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductManagementWebRazor_Temp.Data;
using ProductManagementWebRazor_Temp.Models;

namespace ProductManagementWebRazor_Temp.Pages.Categories
{
    // [BindProperties] // This is to indicate too that all properties will be bonded with the properties of the actions
                     // To not put [BindProperty] on each property
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty] // This attribute means that the Category property will be the same as the Category property of the Post method
        public Category category { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }

        // if you don't use [BindProperty], the action would be public IActionResult OnPost(Category category)
        public IActionResult OnPost() {
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
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["message"] = "Category created successfully";
                TempData["status"] = "success";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
