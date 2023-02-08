using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;

namespace WebShop.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private ApplicationDbContext db;

        public ShoppingCartRepository(ApplicationDbContext _db): base(_db)
        {
            db = _db;
        }

        public void Update(ShoppingCart obj)
        {
            db.ShoppingCarts.Update(obj);
        }
    }
}
