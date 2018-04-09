using MamApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MamApiDb context) : base(context)
        {

        }

        //public User CheckCredential(string userName, string password)
        //{
        //    var user = this.FindByInclude(u => u.UserId == userName && u.Password == password
        //        ,i => i.Position, j => j.Department, k => k.Branch)
        //        .FirstOrDefault();

        //    //if (user != null && user.Count() > 0)
        //    //    return true;
        //    //else
        //    //    return false;

        //    return user;
        //}
    }
}
