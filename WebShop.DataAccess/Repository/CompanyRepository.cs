using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;

namespace WebShop.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private ApplicationDbContext db;

        public CompanyRepository(ApplicationDbContext _db): base(_db)
        {
            db = _db;
        }

        public void Update(Company obj)
        {
            db.Companies.Update(obj);
        }
    }
}
