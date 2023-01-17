using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;
using WebShop.Models.ViewModels;

namespace WebShopProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProductController(IUnitOfWork _unitOfWork, IWebHostEnvironment _hostEnvironment)
        {
            unitOfWork = _unitOfWork;
            hostEnvironment = _hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }


        //GET
        public IActionResult Upsert(int? id)
        {
            ProductViewModel productViewModel = new()
            {
                Product = new(),
                CategoryList = unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                CoverTypeList = unitOfWork.CoverType.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };

            if (id == 0 || id == null)
            {
                //create product
                return View(productViewModel);
            }
            else
            {
                //update product
                productViewModel.Product = unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productViewModel);
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel productObj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = hostEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploadPath = Path.Combine(wwwRootPath, @"images\products");
                    var fileExtension = Path.GetExtension(file.FileName);

                    if(productObj.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, productObj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    //copy file to images/products
                    using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName + fileExtension), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productObj.Product.ImageUrl = @"\images\products\" + fileName + fileExtension;
                }

                if(productObj.Product.Id == 0)
                {
                    unitOfWork.Product.Add(productObj.Product);
                }
                else
                {
                    unitOfWork.Product.Update(productObj.Product);
                }
                unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View(productObj);
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return Json(new {data = productList});
        }


        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productFromDb = unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (productFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting"});
            }

            var oldImagePath = Path.Combine(hostEnvironment.WebRootPath, productFromDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            unitOfWork.Product.Remove(productFromDb);
            unitOfWork.Save();
            return Json(new { success = true, message = "Deleted successfully!"});
        }
        #endregion

    }

}
