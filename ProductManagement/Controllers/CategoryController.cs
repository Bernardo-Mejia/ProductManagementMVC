using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;
using System.Collections.Generic;

namespace ProductManagement.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Category> categoryList = _db.Categories.ToList();
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
                if (_db.Categories.Any(x => x.Name!.ToLower().Equals(category.Name!.ToLower())))
                    ModelState.AddModelError("", "This category already exists");
                if (category.Name!.ToLower().Equals("test") || category.Name!.ToLower().Equals("tests"))
                    ModelState.AddModelError("", $"{category.Name} is not a valid name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if(id == 0)
                return RedirectToAction("Index", "Category");
            Category category = _db.Categories.FirstOrDefault(x => x.Id == id)!;
            return View(category);
        }
    }
}
