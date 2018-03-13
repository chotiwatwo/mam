using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MamApi.Models;

namespace MamApi.Data.Repositories
{
    public class MasterRepository : RepositoryBase<MasterInfo>, IMasterRepository
    {
        public MasterRepository(MamApiDb context) : base(context)
        {

        }
    }
}
