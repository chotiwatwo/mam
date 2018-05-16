using MamApi.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace MamApi.Data.Repositories
{
    public interface IAppRepository : IRepository<MktApplication>
    {
        string GetMaxApplicationIdByUserId(string userId, IDbContextTransaction contextTransaction);

        int GetMaxCustomerId(IDbContextTransaction contextTransaction);

        long InsertApplicationLog(ApplicationLog appLog, IDbContextTransaction contextTransaction);

        MktApplication GetShortApp(string appId);
    }
}
