using System;
using System.Collections.Generic;
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
            string appMaxId = this.Repo.GetMaxApplicationIdByUserId("ST12511"); // ST12511

            //var createdApp = new MktApplication
            //{
            //    AppId = appMaxId
            //};

            app.AppId = appMaxId;
            app.CreatedBy = "Admin";
            app.CreatedDate = DateTime.Now;
            app.AppOwnerId = "ST12511";
            app.BranchId = appMaxId.Substring(0, 2);

            app.Status = "A";
            app.AppStatusPreSubmitDate = DateTime.Now;

            app.Customer.Id = 999998;
            app.Customer.Status = "A";

            Repo.Add(app);

            Repo.Commit();

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
                a => a.AppId.StartsWith("0161"), 
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
