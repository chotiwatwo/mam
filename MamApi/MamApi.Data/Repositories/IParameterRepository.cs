using MamApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Data.Repositories
{
    public interface IParameterRepository : IRepository<Parameter>
    {
        string GetParameterValue(Expression<Func<Parameter, Boolean>> criteria);
    }
}
