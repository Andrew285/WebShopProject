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
    }
}
