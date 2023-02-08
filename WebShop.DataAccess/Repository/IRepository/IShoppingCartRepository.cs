using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository: IRepository<ShoppingCart>
    {
        int IncreamentCount(ShoppingCart shoppingCart, int count);
        int DecreamentCount(ShoppingCart shoppingCart, int count);
    }
}
