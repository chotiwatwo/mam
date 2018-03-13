using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MamApi.Data
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetAll();

        //IEnumerable<T> Query(Func<T, bool> criteria);   Not Work!!!  it gets all rows behind the scene
        IEnumerable<T> Query(Expression<Func<T, Boolean>> criteria);

        T FindByKey(params object[] keyValues);

        //T FindByKeyInclude(params object[] keyValues);
        IEnumerable<T> FindByInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProps);

        T Add(T entity);

        T Remove(T entity);

        void Commit();
    }
}
