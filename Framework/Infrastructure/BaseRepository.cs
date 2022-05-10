using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace Framework.Infrastructure
{
    public class BaseRepository<TKey,T> : IRepository<TKey,T> where T : class
    {
        #region inj

        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        #endregion

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T Get(TKey id)
        {
            return _context.Find<T>(id);
        }

        public bool DoesExist(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression);
        }

        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
