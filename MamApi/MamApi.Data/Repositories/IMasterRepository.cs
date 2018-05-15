using MamApi.Models;
using System.Collections.Generic;

namespace MamApi.Data.Repositories
{
    public interface IMasterRepository : IRepository<MasterInfo>
    {
        IEnumerable<DropDownMasterData> GetDataDropDownMaster(string tableType, string customType);

        IEnumerable<Province> GetMasterDataProvince();

        IEnumerable<Amphur> GetMasterDataAmphur(short provinceId);

        IEnumerable<District> GetMasterDataDistrict(long amphurId);
    }
}
