using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MamApi.Data
{
    public interface IRepository<T> where T : class
    {
        List<T> FetchAll();

        //IEnumerable<T> Query(Func<T, bool> criteria);   Not Work!!!  it gets all rows behind the scene
        IEnumerable<T> Query(Expression<Func<T, Boolean>> criteria);

        T FindByKey(params object[] keyValues);

        T Add(T entity);

        T Remove(T entity);

        void Save();
    }
}
