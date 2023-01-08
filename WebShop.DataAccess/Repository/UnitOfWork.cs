using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DataAccess.Repository.IRepository;

namespace WebShop.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }

        private ApplicationDbContext db;

        public UnitOfWork(ApplicationDbContext _db)
        {
            db = _db;
            Category = new CategoryRepository(_db);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
