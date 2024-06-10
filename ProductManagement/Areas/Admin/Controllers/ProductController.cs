using Microsoft.AspNetCore.Mvc;
using ProductManagement.Models;
using ProductManagement_DataAccess.Repository.IRepository;

namespace ProductManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index() => View(_unitOfWork.ProductRepository.GetAll().ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (product == null)
            {
                return View(product);
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.ProductRepository.Add(product);
                _unitOfWork.Save();
                TempData["message"] = "Product created successfully";
                TempData["status"] = "success";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            if (id != 0)
            {
                Product product = _unitOfWork.ProductRepository.Get(x => x.Id == id);
                if (product == null)
                    return NotFound();
                return View(product);
            }

            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (product == null)
            {
                return View(product);
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductRepository.Update(product);
                _unitOfWork.Save();
                TempData["message"] = "Product updated successfully";
                TempData["status"] = "warning";
                return RedirectToAction("Index", "Product");
            }
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            if (id == 0)
                return RedirectToAction("Index", "Product");
            Product product = _unitOfWork.ProductRepository.Get(x => x.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (id == 0) return NotFound();
            Product product = _unitOfWork.ProductRepository.Get(c => c.Id.Equals(id))!;
            if (product == null) return NotFound();
            _unitOfWork.ProductRepository.Remove(product);
            _unitOfWork.Save();
            TempData["message"] = "Product deleted successfully";
            TempData["status"] = "danger";
            return RedirectToAction("Index", "Product");
        }

    }
}
