using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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

        public ProductController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = unitOfWork.Product.GetAll();
            return View(productList);
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
            }

            return View(productViewModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType type)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CoverType.Update(type);
                unitOfWork.Save();
                TempData["success"] = "Cover Type created successfully";
                return RedirectToAction("Index");
            }
            return View(type);
        }


        //GET
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var typeFromDb = unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            if (typeFromDb == null)
            {
                return NotFound();
            }

            return View(typeFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var typeFromDb = unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (typeFromDb == null)
            {
                return NotFound();
            }


            unitOfWork.CoverType.Remove(typeFromDb);
            unitOfWork.Save();
            TempData["success"] = "Cover Type removed successfully";
            return RedirectToAction("Index");
        }
    }
}
