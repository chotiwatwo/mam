using MamApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MamApi.Data
{
    public class HPPRODb : DbContext
    {
        public HPPRODb(DbContextOptions<HPPRODb> options) : base(options)
        {

        }

        public DbSet<Salesman> Salesman { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Salesman>().ToTable("FNMSMN");

            base.OnModelCreating(builder);
        }

    }
}
