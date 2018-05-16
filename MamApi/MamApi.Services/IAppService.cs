using MamApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MamApi.Services
{
    public interface IAppService
    {
        IEnumerable<MktApplication> GetApps();

        MktApplication GetApp(string appId);

        MktApplication GetAppToCheckNCB(string appId);

        Task<MktApplication> CreateApp(MktApplication app, UserProfile userProfile);

        void UpdateApp();

        void RejectApp();

        void CancelApp();

        void Commit();
    }
}
