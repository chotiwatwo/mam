using MamApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MamApi.Data.Repositories
{
    public class MasterRepository : RepositoryBase<MasterInfo>, IMasterRepository
    {
        private readonly MamApiDb _context;

        public MasterRepository(MamApiDb context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<DropDownMasterData> GetDataDropDownMaster(string tableType, string customType)
        {
            var dropDownMaster = GetDataDropDownMasterUsingStoredProc(tableType, customType);

            return dropDownMaster;
        }

        private IEnumerable<DropDownMasterData> GetDataDropDownMasterUsingStoredProc(string tableType, string customType)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                string maxAppId = string.Empty;

                //command.Transaction = contextTransaction.GetDbTransaction();
                command.CommandText = "dbo.[Sp_C_MKT_DropDownMaster]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@tableType",
                        SqlDbType = SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = tableType
                    }
                );

                command.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@customType",
                        SqlDbType = SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = customType
                    }
                );

                command.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@iErrorCode",
                        SqlDbType = SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output
                    }
                );

                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }

                var result = command.ExecuteReader();

                List<DropDownMasterData> toReturn = new List<DropDownMasterData>();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        toReturn.Add(new DropDownMasterData {
                            Value = result.GetString(0),
                            Text = result.GetString(1)
                        });
                    }
                }

                result.Close();

                return toReturn;
            }
        }

        public IEnumerable<Province> GetMasterDataProvince()
        {
            var provinces = _context.Provinces
                                .Where(p => p.Status == BusinessConstant.StatusActive)
                                .Select(p => new Province
                                    {
                                        Id = p.Id,
                                        Name = p.Name
                                    })
                                .ToList();

            return provinces;
        }

        public IEnumerable<Amphur> GetMasterDataAmphur(short provinceId)
        {
            var amphurs = _context.Amphurs
                              .Where(a => a.ProvinceId == provinceId && a.Status == BusinessConstant.StatusActive)
                              .Select(a => new Amphur
                                  {
                                    Id = a.Id,
                                    Name = a.Name,
                                    ZipCode = a.ZipCode,
                                    ProvinceId = a.ProvinceId
                                  })
                              .ToList();

            return amphurs;
        }

        public IEnumerable<District> GetMasterDataDistrict(long amphurId)
        {
            var districts = _context.Districts
                                .Where(d => d.AmphurId == amphurId && d.Status == BusinessConstant.StatusActive)
                                .Select(d => new District
                                {
                                    Id = d.Id,
                                    Name = d.Name,
                                    AmphurId = d.AmphurId,
                                })
                                .ToList();

            return districts;
        }
    }
}
