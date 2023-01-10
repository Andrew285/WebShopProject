using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;

namespace WebShop.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private ApplicationDbContext db;

        public CoverTypeRepository(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }

        public void Update(CoverType obj)
        {
            db.CoverTypes.Update(obj);
        }
    }
}
