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
        private readonly IMasterRepository _repo;

        public MasterService(IMasterRepository repo)
        {
            _repo = repo;
        }
        
        public IEnumerable<MasterInfo> GetMasterInfosByType(string infoType, string infoStatus)
        {
            var masterInfos = _repo.Query(m => m.Type == infoType && m.Status == infoStatus);

            return masterInfos;
        }
    }
}
