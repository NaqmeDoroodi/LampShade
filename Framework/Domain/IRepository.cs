using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Domain
{
    public interface IRepository <TKey ,T> where T : class
    {
        List<T> GetAll();
        T Get(TKey id);
        bool DoesExist(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Save();
    }
}
