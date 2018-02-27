using MamApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MamApi.Data
{
    public class MktApplicationRepository : RepositoryBase<MktApplication>
    {
        public MktApplicationRepository(MamApiDb context) : base(context)
        {

        }
    }
}
