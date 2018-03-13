using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MamApi.Data.Repositories;
using MamApi.Models;

namespace MamApi.Services
{
    public class MasterService : IMasterService
    {
        private readonly IMasterRepository Repo;

        public MasterService(IMasterRepository repo)
        {
            this.Repo = repo;
        }
        
        public IEnumerable<MasterInfo> GetMasterInfosByType(string infoType, string infoStatus)
        {
            var masterInfos = Repo.Query(m => m.Type == infoType && m.Status == infoStatus);

            return masterInfos;
        }
    }
}
