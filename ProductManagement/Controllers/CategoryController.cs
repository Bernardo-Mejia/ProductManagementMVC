using Microsoft.AspNetCore.Mvc;
using ProductManagement.DataAccess.Data;
using ProductManagement_DataAccess.Repository.IRepository;
using ProductManagement.Models;
using ProductManagement_DataAccess.Repository;

namespace ProductManagement.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Category> categoryList = _categoryRepo.GetAll().ToList();
            return View(categoryList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category == null)
                return View(category);
            if (category.Name != null)
            {
                // Validate if the category name already exists
                if (_categoryRepo.GetAll().Any(x => x.Name!.ToLower().Equals(category.Name!.ToLower())))
                    ModelState.AddModelError("", "This category already exists");
                if (category.Name!.ToLower().Equals("test") || category.Name!.ToLower().Equals("tests"))
                    ModelState.AddModelError("", $"{category.Name} is not a valid name");
            }
            if (ModelState.IsValid)
            {
                _categoryRepo.Add(category);
                _categoryRepo.Save();
                TempData["message"] = "Category created successfully";
                TempData["status"] = "success";
                return RedirectToAction("Index", "Category");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id == 0)
                return RedirectToAction("Index", "Category");
            Category category = _categoryRepo.Get(x => x.Id == id);
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id != 0)
            {
                Category category = _categoryRepo.Get(c => c.Id == id)!;
                if (category == null)
                    return NotFound();
                return View(category);
            }
            return RedirectToAction("Index", "Category");
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category == null)
                return View(category);

            if (string.IsNullOrEmpty(category.Name))
            {
                ModelState.AddModelError("", "Category name cannot be empty");
                return View(category);
            }

            // Validate if the category name already exists
            if (_categoryRepo.GetAll().Any(x => x.Name!.ToLower().Equals(category.Name!.ToLower()) && x.Id != category.Id))
                ModelState.AddModelError("", "This category already exists");

            if (category.Name!.ToLower().Equals("test") || category.Name!.ToLower().Equals("tests"))
                ModelState.AddModelError("", $"{category.Name} is not a valid name");

            if (ModelState.IsValid)
            {
                // Fetch the existing entity from the database
                Category existingCategory = _categoryRepo.GetById(category.Id);
                if (existingCategory != null)
                {
                    // Update the properties of the existing entity
                    existingCategory.Name = category.Name;
                    existingCategory.DisplayOrder = category.DisplayOrder;

                    _categoryRepo.Update(existingCategory);
                    _categoryRepo.Save();
                    TempData["message"] = "Category updated successfully";
                    TempData["status"] = "warning";
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Category not found");
                }
            }

            return View(category);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == 0)
                return RedirectToAction("Index", "Category");
            Category category = _categoryRepo.Get(x => x.Id == id);
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == 0) return NotFound();
            Category category = _categoryRepo.Get(c => c.Id.Equals(id))!;
            if (category == null) return NotFound();
            _categoryRepo.Remove(category);
            _categoryRepo.Save();
            TempData["message"] = "Category deleted successfully";
            TempData["status"] = "danger";
            return RedirectToAction("Index", "Category");
        }
    }
}
