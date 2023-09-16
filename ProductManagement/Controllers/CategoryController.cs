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
        public IActionResult Index()
        {
            List<Category> categoryList = _db.Categories.ToList();
            return View(categoryList);
        }
    }
}
