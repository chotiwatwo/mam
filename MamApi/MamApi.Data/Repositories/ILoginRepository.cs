using MamApi.Models;

namespace MamApi.Data.Repositories
{
    public interface ILoginRepository : IRepository<CurrentLoginMAM>
    {
        CurrentLoginMAM GetActiveCurrentLogin(string userId);
    }
}
