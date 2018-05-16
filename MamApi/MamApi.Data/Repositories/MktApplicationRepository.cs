using System;
using MamApi.Data.Repositories;
using MamApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MamApi.Data
{
    public class MktApplicationRepository : RepositoryBase<MktApplication>, IAppRepository
    {
        private readonly MamApiDb _context;

        public MktApplicationRepository(MamApiDb context) : base(context)
        {
            _context = context;
        }

        private string GetMaxApplicationIdUsingStoredProc(string userId, IDbContextTransaction contextTransaction)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                string maxAppId = string.Empty;

                command.Transaction = contextTransaction.GetDbTransaction();
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

        public string GetMaxApplicationIdByUserId(string userId, 
            IDbContextTransaction contextTransaction)
        {
            string maxAppId = GetMaxApplicationIdUsingStoredProc(userId, contextTransaction);

            return maxAppId;
        }

        public int GetMaxCustomerId(IDbContextTransaction contextTransaction)
        {
            int maxCustomerId = GetMaxCustomerIdUsingStoredProc(contextTransaction);

            return maxCustomerId;
        }

        private int GetMaxCustomerIdUsingStoredProc(IDbContextTransaction contextTransaction)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.Transaction = contextTransaction.GetDbTransaction();
                command.CommandText = "dbo.[Sp_C_MKT_Customer_GetMax_MKT_CustomerID]";
                command.CommandType = CommandType.StoredProcedure;

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

                int maxCustomerId = 0;
                var result = command.ExecuteReader();

                if (result.HasRows)
                {
                    while (result.Read())
                    {
                        maxCustomerId = result.GetInt32(0);
                    }
                }

                result.Close();

                return maxCustomerId;
            }
        }

        public long InsertApplicationLog(ApplicationLog appLog, IDbContextTransaction contextTransaction)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.Transaction = contextTransaction.GetDbTransaction();
                command.CommandText = "dbo.[Sp_G_ApplicationLog_Insert]";
                command.CommandType = CommandType.StoredProcedure;

                /* 
                 
                exec dbo.[Sp_G_ApplicationLog_Insert] 
                  @ApplicationLog_AppID='0161003050',@ApplicationLog_FromUserID='ST12511',
                  @ApplicationLog_FromLevelID='GL00000001',@ApplicationLog_FromDepartmentID='DEPT000002',
                  @ApplicationLog_FromBranchID='01',
                  
               
                
                   @ApplicationLog_Date='2018-04-17 11:34:31.270',
                   @ApplicationLog_AppStatus='P',
                
                   @ApplicationLog_Remark='Create App',
                   @ApplicationLog_ActionName='Create App',
                   @ApplicationLog_Status='A',
                   @ApplicationLog_CreateBy='ST12511',
                   @ApplicationLog_CreateDate='2018-04-17 11:34:31.273',
                
  
                @ApplicationLog_ID=@p19 output,@ErrorCode=@p20 output

                */

                command.Parameters.Add(
                    new SqlParameter("@ApplicationLog_AppID", SqlDbType.VarChar, 10)
                    {
                        IsNullable = false,
                        Value = appLog.AppId
                    });

                command.Parameters.Add(
                    new SqlParameter("@ApplicationLog_FromUserID", SqlDbType.VarChar, 20)
                    {
                        IsNullable = false,
                        Value = appLog.FromUserId
                    });

                command.Parameters.Add(
                    new SqlParameter("@ApplicationLog_FromLevelID", SqlDbType.VarChar, 10)
                    {
                        IsNullable = false,
                        Value = appLog.FromLevelId
                    });

                command.Parameters.Add(
                    new SqlParameter("@ApplicationLog_FromDepartmentID", SqlDbType.VarChar, 10)
                    {
                        IsNullable = false,
                        Value = appLog.FromDepartmentID
                    });

                command.Parameters.Add(
                    new SqlParameter("@ApplicationLog_FromBranchID", SqlDbType.VarChar, 10)
                    {
                        IsNullable = true,
                        Value = appLog.FromBranchID
                    });

                command.Parameters.Add(
                    new SqlParameter("@ApplicationLog_ToUserID", SqlDbType.VarChar, 20)
                    {
                        IsNullable = true,
                        Value = DBNull.Value
                    });

                command.Parameters.Add(
                    new SqlParameter("@ApplicationLog_ToLevelID", SqlDbType.VarChar, 10)
                    {
                        IsNullable = true,
                        Value = DBNull.Value
                    });

                command.Parameters.Add(
                    new SqlParameter("@ApplicationLog_ToDepartmentID", SqlDbType.VarChar, 10)
                    {
                        IsNullable = true,
                        Value = DBNull.Value
                    });

                command.Parameters.Add(
                    new SqlParameter("@ApplicationLog_Date", SqlDbType.DateTime, 8)
                    {
                        IsNullable = false,
                        Value = appLog.AppLogDate
                    });

                command.Parameters.Add(
                    new SqlParameter("@ApplicationLog_AppStatus", SqlDbType.Char, 1)
                    {
                        IsNullable = false,
                        Value = appLog.AppStatus
                    });

                command.Parameters.Add(
                    new SqlParameter("@ApplicationLog_RemarkID", SqlDbType.VarChar, 10)
                    {
                        IsNullable = true,
                        Value = DBNull.Value
                    });

                command.Parameters.Add(
                    new SqlParameter("@ApplicationLog_Remark", SqlDbType.VarChar, 200)
                    {
                        IsNullable = true,
                        Value = appLog.Remark
                    });

                command.Parameters.Add(
                    new SqlParameter("@ApplicationLog_ActionName", SqlDbType.VarChar, 50)
                    {
                        IsNullable = true,
                        Value = appLog.ActionName
                    });

                command.Parameters.Add(
                    new SqlParameter("@ApplicationLog_Status", SqlDbType.Char, 1)
                    {
                        IsNullable = false,
                        Value = appLog.Status
                    });

                command.Parameters.Add(
                    new SqlParameter("@ApplicationLog_CreateBy", SqlDbType.VarChar, 20)
                    {
                        IsNullable = true,
                        Value = appLog.CreateBy
                    });

                command.Parameters.Add(
                    new SqlParameter("@ApplicationLog_CreateDate", SqlDbType.DateTime, 8)
                    {
                        IsNullable = true,
                        Value = appLog.CreateDate
                    });

                command.Parameters.Add(
                    new SqlParameter("@ApplicationLog_UpdateBy", SqlDbType.VarChar, 20)
                    {
                        IsNullable = true,
                        Value = DBNull.Value
                    });

                command.Parameters.Add(
                    new SqlParameter("@ApplicationLog_UpdateDate", SqlDbType.DateTime, 8)
                    {
                        IsNullable = true,
                        Value = DBNull.Value
                    });

                SqlParameter outputParam_LogId = new SqlParameter("@ApplicationLog_ID", SqlDbType.BigInt, 8) { 
                    Direction = ParameterDirection.Output
                };
                
                command.Parameters.Add(outputParam_LogId);

                SqlParameter outputParam_ErrorCode = new SqlParameter("@ErrorCode", SqlDbType.Int, 4) { 
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(outputParam_ErrorCode);

                if (command.Connection.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }

                // Execute query.
                int _rowsAffected = command.ExecuteNonQuery();
                long _applicationLog_ID = Convert.ToInt64(command.Parameters["@ApplicationLog_ID"].Value.ToString());
                int _errorCode = Convert.ToInt32(command.Parameters["@ErrorCode"].Value.ToString());

                appLog.AppLogId = _applicationLog_ID;

                return _applicationLog_ID;
            }

        }

        public MktApplication GetShortApp(string appId)
        {
            MktApplication app = _context.MktApplications
                                     .Where(a => a.AppId == appId)

                                     .Include(a => a.Customer)
                                     .ThenInclude(c => c.Addresses)
                                     .ThenInclude(ad => ad.District)
                                     .ThenInclude(dt => dt.Amphur)
                                     //.ThenInclude(am => am.Province)

                                     //.Include(a => a.Customer)
                                     //.ThenInclude(c => c.Addresses)
                                     //.ThenInclude(ad => ad.Amphur)

                                     .SingleOrDefault();

            return app;
        }
    }
}
