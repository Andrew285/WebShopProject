using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using WebShop.DataAccess;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;
using WebShop.Models.ViewModels;

namespace WebShopProject.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CompanyController(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }


        //GET
        public IActionResult Upsert(int? id)
        {
            Company company = new Company();

            if (id == 0 || id == null)
            {
                //create product
                return View(company);
            }
            else
            {
                //update product
                company = unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
                return View(company);
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    unitOfWork.Company.Add(company);
                    TempData["success"] = "Company created successfully";

                }
                else
                {
                    unitOfWork.Company.Update(company);
                    TempData["success"] = "Company updated successfully";

                }
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(company);
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = unitOfWork.Company.GetAll();
            return Json(new { data = companyList });
        }


        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyFromDb = unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
            if (companyFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            unitOfWork.Company.Remove(companyFromDb);
            unitOfWork.Save();
            return Json(new { success = true, message = "Deleted successfully!" });
        }
        #endregion

    }
}
