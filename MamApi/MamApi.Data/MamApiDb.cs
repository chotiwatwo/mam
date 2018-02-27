using MamApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MamApi.Data
{
    public class MamApiDb : DbContext
    {
        public MamApiDb(DbContextOptions<MamApiDb> options) : base(options)
        {

        }

        public DbSet<MktApplication> MktApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
              .Entity<MktApplication>()
              .ToTable("MKT_Application");
        }
    }
}
