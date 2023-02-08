using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;

namespace WebShop.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext db;

        public ApplicationUserRepository(ApplicationDbContext _db): base(_db)
        {
            db = _db;
        }

        public void Update(ApplicationUser obj)
        {
            db.ApplicationUsers.Update(obj);
        }
    }
}
