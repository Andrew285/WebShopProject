using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WebShop.DataAccess.Repository.IRepository
{
    interface IRepository<T> where T: class
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}
