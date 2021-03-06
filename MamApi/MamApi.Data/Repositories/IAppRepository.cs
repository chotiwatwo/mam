﻿using MamApi.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;

namespace MamApi.Data.Repositories
{
    public interface IAppRepository : IRepository<MktApplication>
    {
        string GetMaxApplicationIdByUserId(string userId, IDbContextTransaction contextTransaction);

        int GetMaxCustomerId(IDbContextTransaction contextTransaction);

        long InsertApplicationLog(ApplicationLog appLog, IDbContextTransaction contextTransaction);

        MktApplication GetShortApp(string appId);

        MktApplication GetFullApp(string appId);

        IEnumerable<object> GetAppsByPage(int pageNo, int pageSize);

        int UpdateApplicationCurrentCarId(long currentCarId, string appId, IDbContextTransaction contextTransaction);

        int UpdateFastTrack(string appId, IDbContextTransaction contextTransaction);
    }
}
