using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebShopProject.Data;
using WebShopProject.Models;

namespace WebShopProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext db;

        public CategoryController(ApplicationDbContext _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = db.Categories;
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
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}
