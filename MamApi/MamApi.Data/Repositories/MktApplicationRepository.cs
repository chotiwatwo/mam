using System;
using System.Collections.Generic;
using System.Text;
using MamApi.Data.Repositories;
using MamApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace MamApi.Data
{
    public class MktApplicationRepository : RepositoryBase<MktApplication>, IAppRepository
    {
        private readonly MamApiDb Context;

        public MktApplicationRepository(MamApiDb context) : base(context)
        {
            this.Context = context;
        }

        private string GetMaxApplicationIdUsingStoredProc(string userId)
        {
            using (var command = this.Context.Database.GetDbConnection().CreateCommand())
            {
                string maxAppId = string.Empty;

                command.CommandText = "dbo.[Sp_C_MKT_Application_GetMax_MKT_ApplicationID]";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(
                    new SqlParameter()
                    {
                        ParameterName = "@User_UserID",
                        SqlDbType = SqlDbType.VarChar,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = userId
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

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        maxAppId = result.GetString(0);
                    }
                }

                result.Close();

                return maxAppId;
            }
        }

        public string GetMaxApplicationIdByUserId(string userId)
        {
            string MaxAppId = GetMaxApplicationIdUsingStoredProc(userId);

            return MaxAppId;
        }
    }
}
