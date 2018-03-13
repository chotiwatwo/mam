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

        //public string ExecSP(string sqlStatement) {

        //    using (var command = _context.Database.GetDbConnection().CreateCommand())
        //    {
        //        string maxAppId = string.Empty;

        //        command.CommandText = "dbo.[Sp_C_MKT_Application_GetMax_MKT_ApplicationID]";
        //        command.CommandType = CommandType.StoredProcedure;

        //        command.Parameters.Add(
        //            new SqlParameter()
        //            {
        //                ParameterName = "@User_UserID",
        //                SqlDbType = SqlDbType.VarChar,
        //                Direction = System.Data.ParameterDirection.Input,
        //                Value = sqlStatement
        //            }
        //        );

        //        command.Parameters.Add(
        //            new SqlParameter()
        //            {
        //                ParameterName = "@iErrorCode",
        //                SqlDbType = SqlDbType.Int,
        //                Direction = System.Data.ParameterDirection.Output
        //            }
        //        );

        //        if (command.Connection.State != ConnectionState.Open)
        //        {
        //            command.Connection.Open();
        //        }

        //        //_context.Database.OpenConnection();
        //        var result = command.ExecuteReader();
                
        //        // do something with result
        //        if (result.HasRows)
        //        {
        //            while (result.Read())
        //            {
        //                maxAppId = result.GetString(0);
        //            }
        //        }

        //        result.Close();

        //        return maxAppId;
        //    }
            
        //    /*
        //    SqlParameter[] SqlParams = {
        //        new SqlParameter() {
        //            ParameterName = "@User_UserID",
        //            SqlDbType = SqlDbType.VarChar,
        //            Direction = System.Data.ParameterDirection.Input,
        //            Value = sqlStatement
        //        },

        //        new SqlParameter() {
        //            ParameterName = "@iErrorCode",
        //            SqlDbType = SqlDbType.Int,
        //            Direction = System.Data.ParameterDirection.Output
        //        }
        //    };

        //    int res = _context.Database.ExecuteSqlCommand("exec dbo.[Sp_C_MKT_Application_GetMax_MKT_ApplicationID] @User_UserID, @iErrorCode", SqlParams);


        //    string maxApp = SqlParams[1].ToString();
        //    */
        //    //return maxApp;
        //}

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

        private IQueryable<T> GetAllIncluding
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

        public void Commit()
        {
            _context.SaveChanges();
        }

        public DbSet<T> GetDbSet()
        {
            return this.DbSet;
        }

        #endregion

        #region Async Member
        // For Async Version
        #endregion
    }
}
