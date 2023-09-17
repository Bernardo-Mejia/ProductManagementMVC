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
            Category category = _db.Categories.FirstOrDefault(x => x.Id == id)!;
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id != 0)
            {
                Category category = _db.Categories.FirstOrDefault(c => c.Id == id)!;
                if(category == null)
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
                return RedirectToAction("Index", "Category");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id==0)
                return RedirectToAction("Index", "Category");
            Category category = _db.Categories.Find(id)!;
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == 0) return NotFound();
            Category category = _db.Categories.Where(c => c.Id.Equals(id)).FirstOrDefault()!;
            if (category == null) return NotFound();
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["message"] = "Category deleted successfully";
            TempData["status"] = "danger";
            return RedirectToAction("Index", "Category");
        }
    }
}
