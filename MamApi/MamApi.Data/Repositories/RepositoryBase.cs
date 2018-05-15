﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace MamApi.Data
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        #region Properties
        private readonly MamApiDb _context;
        protected MamApiDb DbContext
        {
            get
            {
                return _context;
            }
        }

        private DbSet<T> _dbSet;
        protected DbSet<T> DbSet
        {
            get
            {
                if (_dbSet == null)
                    _dbSet = _context.Set<T>();

                return _dbSet;
            }
        }
       
        #endregion

        #region Constructor
        public RepositoryBase(MamApiDb context)
        {
            _context = context;
        }
        #endregion

        #region Regular Member

        public IList<T> GetAll()
        {
            return this.DbSet.Take(100).ToList();
        }
        
        public IEnumerable<T> Query(Expression<Func<T, Boolean>> criteria)
        {
            return this.DbSet.Where(criteria).AsEnumerable();
        }

        public T FindByKey(params object[] keyValues) {
            return this.DbSet.Find(keyValues);
        }

        public IEnumerable<T> FindByInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProps) {
            var query = GetAllIncluding(includeProps);
            
            IEnumerable<T> results = query.Where(predicate).ToList();

            return results;
        }

        public IQueryable<T> GetAllIncluding
        (params Expression<Func<T, object>>[] includeProps)
        {
            IQueryable<T> queryable = this.DbSet;

            return includeProps.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));
        } 
        
        public T Add(T entity)
        {
            return this.DbSet.Add(entity).Entity;
        }

        public T Remove(T entity)
        {
            return this.DbSet.Remove(entity).Entity;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
             return await _context.SaveChangesAsync();
        }

        public DbSet<T> GetDbSet()
        {
            return this.DbSet;
        }

        /// <summary>
        /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
        /// </summary>
        /// <param name="selector">The selector for projection.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="orderBy">A function to order elements.</param>
        /// <param name="include">A function to include navigation properties</param>
        /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
        /// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
        /// <remarks>This method default no-tracking query.</remarks>
        public TResult GetFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector,
                                                  Expression<Func<T, bool>> predicate = null,
                                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                  Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                  bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).Select(selector).FirstOrDefault();
            }
            else
            {
                return query.Select(selector).FirstOrDefault();
            }
        }

        #endregion

        #region Async Member
        // For Async Version
        #endregion
    }
}
