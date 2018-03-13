using MamApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Services
{
    public interface IMasterService
    {
        IEnumerable<MasterInfo> GetMasterInfosByType(string infoType, string infoStatus);
    }
}
