using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;

namespace WebShop.DataAccess.Repository
{
    class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext db;

        public CategoryRepository(ApplicationDbContext _db): base(_db)
        {
            db = _db;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Category obj)
        {
            db.Categories.Update(obj);
        }
    }
}
