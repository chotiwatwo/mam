using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MamApi.Data.Repositories;
using MamApi.Models;

namespace MamApi.Services
{
    public class AppService : IAppService
    {
        private readonly IAppRepository Repo;

        public AppService(IAppRepository repo)
        {
            this.Repo = repo;
        }

        public void CancelApp()
        {
            
        }

        public void Commit()
        {

        }

        public MktApplication CreateApp(MktApplication app)
        {
            //var context = this.Repo.GetDbContext();

            //if (context != null) {
            //    context.Database.
            //}

            string appMaxId = this.Repo.GetMaxApplicationIdByUserId("SJ13411"); // ST12511

            app.MKT_Application_ID = appMaxId;

            return app;
        }

        public MktApplication GetApp(string appNo)
        {
            var app = Repo.FindByKey(appNo);

            return app;
        }

        public IEnumerable<MktApplication> GetApps()
        {
            var apps = Repo.FindByInclude(
                a => a.MKT_Application_ID.StartsWith("0161"), 
                b => b.Branch);

            return apps;
        }

        public void RejectApp()
        {

        }

        public void UpdateApp()
        {

        }
    }
}
