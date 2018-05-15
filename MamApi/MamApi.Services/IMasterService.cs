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

        IEnumerable<MasterInfo> GetMasterInfosByIdAndType(string infoId, string infoType, string infoStatus);

        IEnumerable<DropDownMasterData> GetMasterLoanTypes();

        IEnumerable<DropDownMasterData> GetYesOrNoOptions();

        IEnumerable<DropDownMasterData> GetGroupOccupationTypeOptions();

        IEnumerable<Province> GetProvinces();

        IEnumerable<Amphur> GetAmphurs(short provinceId);

        IEnumerable<District> GetDistricts(long amphurId);
    }
}
