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

        public IEnumerable<MasterInfo> GetMasterInfosByIdAndType(string infoId, string infoType, string infoStatus)
        {
            var masterInfos = _repo.Query(m => m.Id == infoId && m.Type == infoType && m.Status == infoStatus);

            return masterInfos;
        }

        public IEnumerable<DropDownMasterData> GetMasterLoanTypes()
        {
            var masterLoanTypes = _repo.GetDataDropDownMaster("", "FLoanType");

            return masterLoanTypes;
        }

        public IEnumerable<DropDownMasterData> GetYesOrNoOptions() {
            List<DropDownMasterData> yesOrNoOptions = new List<DropDownMasterData>() {
                new DropDownMasterData { Value = "0", Text = "No" },
                new DropDownMasterData { Value = "1", Text = "Yes" }
            };

            return yesOrNoOptions;
        }

        public IEnumerable<DropDownMasterData> GetGroupOccupationTypeOptions() {
            List<DropDownMasterData> groupOccupationTypeOptions = new List<DropDownMasterData>()
            {
                new DropDownMasterData { Value = "01", Text = "Salaried" },
                new DropDownMasterData { Value = "02", Text = "Self Employed" },
                new DropDownMasterData { Value = "03", Text = "Freelance" }
            };

            return groupOccupationTypeOptions;
        }

        public IEnumerable<Province> GetProvinces()
        {
            var provinces = _repo.GetMasterDataProvince();

            return provinces;
        }

        public IEnumerable<Amphur> GetAmphurs(short provinceId)
        {
            var amphurs = _repo.GetMasterDataAmphur(provinceId);

            return amphurs;
        }

        public IEnumerable<District> GetDistricts(long amphurId)
        {
            var districts = _repo.GetMasterDataDistrict(amphurId);

            return districts;
        }
    }
}
