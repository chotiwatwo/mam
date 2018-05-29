using MamApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Data.Repositories
{
    public class ParameterRepository : RepositoryBase<Parameter>, IParameterRepository
    {
        private readonly MamApiDb _context;

        public ParameterRepository(MamApiDb context) : base(context)
        {
            _context = context;
        }

        public string GetParameterValue(Expression<Func<Parameter, bool>> criteria)
        {
            string parameterValue = this.DbSet.Where(criteria).Select(p => p.Value).SingleOrDefault();

            return parameterValue;
        }
    }
}
