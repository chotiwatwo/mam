using Microsoft.EntityFrameworkCore;
using System;

namespace MamApi.Data
{
    public class HPCSDb : DbContext
    {
        public HPCSDb(DbContextOptions<HPCSDb> options) : base(options)
        {

        }


    }
}
