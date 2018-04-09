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
        public DbSet<MasterInfo> MasterInfos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
    
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
                .Entity<MasterInfo>()
                .ToTable("MasterInfo")
                .HasKey(m => new { m.Id, m.Type });

            modelBuilder
                .Entity<User>()
                .ToTable("User");
                //.HasOne(u => u.Position);
                
            modelBuilder
                .Entity<Position>()
                .ToTable("Position");

            modelBuilder
                .Entity<Department>()
                .ToTable("Department");

            modelBuilder
                .Entity<Branch>()
                .ToTable("Branch");
        }
    }
}
