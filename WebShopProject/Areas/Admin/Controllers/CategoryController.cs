using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebShop.DataAccess;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;

namespace WebShopProject.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = unitOfWork.Category.GetAll();
            return View(categoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                unitOfWork.Category.Add(category);
                unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }


        //GET
        public IActionResult Edit(int? id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }

            var categoryFromDb = unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            
            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                unitOfWork.Category.Update(category);
                unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }


        //GET
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var categoryFromDb = unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var categoryFromDb = unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }


            unitOfWork.Category.Remove(categoryFromDb);
            unitOfWork.Save();
            TempData["success"] = "Category removed successfully";
            return RedirectToAction("Index");
        }

    }
}
