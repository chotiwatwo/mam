using MamApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Data.Repositories
{
    public interface ICreditCheckingRepository : IRepository<CcCreditChk>
    {
        void SaveConsentReceiveStatus(int creditCheckingId, string userId);
    }
}
