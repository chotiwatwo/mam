using MamApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MamApi.Data.Repositories
{
    public interface IAppRepository : IRepository<MktApplication>
    {
        string GetMaxApplicationIdByUserId(string userId);
    }
}
