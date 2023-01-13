using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;

namespace WebShop.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext db;

        public ProductRepository(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }

        public void Update(Product obj)
        {
            var objFromDb = db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if(objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Price = obj.Price;
                objFromDb.SpecialPrice10 = obj.SpecialPrice10;
                objFromDb.SpecialPrice5 = obj.SpecialPrice5;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.CoverTypeId = obj.CoverTypeId;
                if(obj.ImageUrl != null) 
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
