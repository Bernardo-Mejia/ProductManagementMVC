using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductManagement.Models;
using ProductManagement.Models.ViewModels;
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

        public IActionResult Index()
        {
            List<Product> productsList = _unitOfWork.ProductRepository.GetAll().ToList();
            return View(productsList);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> categoryList = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            #region ViewData
            /*
            ViewData["CategoryList"] = categoryList;
             <select asp-for="CategoryId" asp-items="@(ViewData["CategoryList"] as IEnumerable<SelectListItem>)" class="form-select border-0 shadow">
                   <option disabled selected>-- Select Category --</option>
             </select>
             */
            #endregion

            #region ViewBag
            //ViewBag.CategoryList = categoryList;
            /*
             <select asp-for="CategoryId" asp-items="ViewBag.CategoryList" class="form-select border-0 shadow">
                   <option disabled selected>-- Select Category --</option>
             </select>
             */
            #endregion

            ProductVM productVM = new()
            {
                CategoryList = categoryList,
                Product = new()
            };

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.ProductRepository.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["message"] = "Product created successfully";
                TempData["status"] = "success";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                productVM.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
                TempData["message"] = "Please, validate data";
                TempData["status"] = "warning";
            }
            return View(productVM);
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
