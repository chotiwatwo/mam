using MamApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MamApi.Data
{
    public class MamApiDb : DbContext
    {
        public DbSet<MktApplication> MktApplications { get; set; }
        public DbSet<MktCustomer> MktCustomers { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<MasterInfo> MasterInfo { get; set; }

        public MamApiDb(DbContextOptions<MamApiDb> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<MktApplication>()
                .ToTable("MKT_Application");

            modelBuilder
                .Entity<MktCustomer>()
                .ToTable("MKT_Customer");

            modelBuilder
                .Entity<Branch>()
                .ToTable("Branch");

            modelBuilder
                .Entity<MasterInfo>()
                .ToTable("MasterInfo")
                .HasKey(m => new { m.Id, m.Type });



        }
    }
}
