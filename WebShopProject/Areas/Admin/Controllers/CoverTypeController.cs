using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;

namespace WebShopProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CoverTypeController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> coverTypeList = unitOfWork.CoverType.GetAll();
            return View(coverTypeList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType type)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.CoverType.Add(type);
                unitOfWork.Save();
                TempData["success"] = "Cover Type created successfully";
                return RedirectToAction("Index");
            }
            return View(type);
        }


        //GET
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var coverTypeFromDb = unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            if (coverTypeFromDb == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType type)
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
