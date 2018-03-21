using MamApi.Models;
using System.Collections.Generic;

namespace MamApi.Services
{
    public interface IAppService
    {
        IEnumerable<MktApplication> GetApps();

        MktApplication GetApp(string appNo);

        MktApplication CreateApp(MktApplication app);

        void UpdateApp();

        void RejectApp();

        void CancelApp();

        void Commit();
    }
}
