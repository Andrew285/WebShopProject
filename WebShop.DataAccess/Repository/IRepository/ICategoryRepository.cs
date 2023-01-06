using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.DataAccess.Repository.IRepository
{
    interface ICategoryRepository: IRepository<Category>
    {
        void Update(Category obj);
        void Save();
    }
}
