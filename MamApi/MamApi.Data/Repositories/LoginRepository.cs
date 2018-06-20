using MamApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Data.Repositories
{
    public class LoginRepository : RepositoryBase<CurrentLoginMAM>, ILoginRepository
    {
        public LoginRepository(MamApiDb context) : base(context)
        {

        }

        // User ที่ Login เข้ามา และยังไม่ได้ Logout  ให้ check จาก
        // field [LogoutTime]   หรือเป็นค่าว่าง ก็ได้
        public CurrentLoginMAM GetActiveCurrentLogin(string userId)
        {
            var activeCurrentLogin = this
                .Query(l => l.UserID == userId && // l.LogoutTime == l.LoginTime)
                      !l.LogoutTime.HasValue)
                      //(l.LogoutTime == l.LoginTime || !l.LogoutTime.HasValue))
                .FirstOrDefault();

            return activeCurrentLogin;
        }
    }
}
