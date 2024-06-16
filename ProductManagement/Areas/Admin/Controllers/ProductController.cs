using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> productsList = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category").ToList();
            return View(productsList);
        }

        #region This will be replaced by Upsert method

        [Obsolete]
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
        [Obsolete]
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

        [Obsolete]
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
        [Obsolete]
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

        #endregion End Create/Edit

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Product = new()
            };

            if (id == null || id == 0)
                // Create
                return View(productVM);
            else
            {
                // Update
                productVM.Product = _unitOfWork.ProductRepository.Get(p => p.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!String.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        // Delete Old Image
                        string oldPathImage = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldPathImage))
                        {
                            System.IO.File.Delete(oldPathImage);
                        }
                    }

                    using (FileStream fileStream = new(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                string messageData = String.Empty,
                    statusData = String.Empty;

                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.ProductRepository.Add(productVM.Product);
                    messageData = $"Product created successfully";
                    statusData = $"success";
                }
                else
                {
                    messageData = $"Product updated successfully";
                    statusData = $"warning";
                    _unitOfWork.ProductRepository.Update(productVM.Product);
                }

                _unitOfWork.Save();
                TempData["message"] = messageData;
                TempData["status"] = statusData;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                productVM.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
                TempData["message"] = "Please, validate data";
                TempData["status"] = "warning";

                return View(productVM);
            }
        }

        #region Replace with HttpDelete
        /*
        [Obsolete]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return RedirectToAction("Index", "Product");
            Product product = _unitOfWork.ProductRepository.Get(x => x.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
        [Obsolete]
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
        */
        #endregion

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = products });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            Product product = _unitOfWork.ProductRepository.Get(c => c.Id.Equals(id))!;
            if (product == null) return NotFound();

            string oldPathImage = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldPathImage))
            {
                System.IO.File.Delete(oldPathImage);
            }

            _unitOfWork.ProductRepository.Remove(product);
            _unitOfWork.Save();
            //TempData["message"] = "Product deleted successfully";
            //TempData["status"] = "danger";
            //return RedirectToAction("Index", "Product");

            return Json(new { success = true, message = "Delete Succesfully" });
        }

        #endregion

    }
}
