using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MamApi.Data
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly MamApiDb _context;

        public RepositoryBase(MamApiDb context)
        {
            _context = context;
        }

        public List<T> FetchAll()
        {
            return _context.Set<T>().Take(100).ToList();
        }
        
        public IEnumerable<T> Query(Expression<Func<T, Boolean>> criteria)
        {
            return _context.Set<T>().Where(criteria).AsEnumerable();
        }

        public T FindByKey(params object[] keyValues) {
            return _context.Set<T>().Find(keyValues);
        }

        public T Add(T entity)
        {
            return _context.Set<T>().Add(entity).Entity;
        }

        public T Remove(T entity)
        {
            return _context.Set<T>().Remove(entity).Entity;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
