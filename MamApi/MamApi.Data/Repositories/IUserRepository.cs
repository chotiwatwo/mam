using MamApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        //User CheckCredential(string userName, string password);
    }
}
