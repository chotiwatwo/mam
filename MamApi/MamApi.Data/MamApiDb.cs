using MamApi.Models;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging.Console;
using System;

namespace MamApi.Data
{
    public class MamApiDb : DbContext
    {
        //public static readonly LoggerFactory loggerFactory
        //    = new LoggerFactory(new[] {
        //      new ConsoleLoggerProvider(
        //        (category, level) => category == DbLoggerCategory.Database.Command.Name && level >= LogLevel.Information, true)
        //    });

        public DbSet<MktApplication> MktApplications { get; set; }
        public DbSet<MktApplicationExtend> MktApplicationExtends { get; set; }
        public DbSet<MktCustomer> MktCustomers { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<MasterInfo> MasterInfos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<GroupLevel> GroupLevels { get; set; }
        public DbSet<MktAnnotation> MktAnnotations { get; set; }
        public DbSet<MktAddress> MktAddresses { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Amphur> Amphurs { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Parameter> Parameters { get; set; }

        public MamApiDb(DbContextOptions<MamApiDb> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLoggerFactory(loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region <<< MKT Application >>>
            var appEntity = modelBuilder.Entity<MktApplication>().ToTable("MKT_Application");

            // Map One to One relationship โดยไม่ต้องฝังไว้ที่ Model
            appEntity
                .HasOne(a => a.ApplicationExtend)
                .WithOne()
                .HasForeignKey<MktApplicationExtend>(b => b.AppId);

            // Map One to One relationship โดยไม่ต้องฝังไว้ที่ Model
            appEntity
                .HasOne(a => a.Annotation)
                .WithOne()
                .HasForeignKey<MktAnnotation>(b => b.AppId);

            modelBuilder
                .Entity<MktApplicationExtend>()
                .ToTable("MKT_ApplicationExtend");

            modelBuilder
                .Entity<MktAnnotation>()
                .ToTable("MKT_Annotation");

            modelBuilder
                .Entity<Attachment>()
                .ToTable("Attachment")
                .HasKey(a => new { a.Id, a.AppId, a.CustomerId });

            #endregion <<< MKT Application >>>

            #region <<< MKT Customer >>>
            var customerEntity = modelBuilder.Entity<MktCustomer>().ToTable("MKT_Customer");

            customerEntity
                .HasOne(c => c.Asset)
                .WithOne()
                .HasForeignKey<MktAsset>(a => a.CustomerId);

            modelBuilder
                .Entity<MktAsset>()
                .ToTable("MKT_Asset")
                .HasKey(a => new { a.Id, a.CustomerId });

            customerEntity
                .HasMany(c => c.Addresses)
                .WithOne()
                .HasForeignKey(a => a.CustomerId);

            var addressEntity = modelBuilder.Entity<MktAddress>().ToTable("MKT_Address");

            addressEntity
                .HasKey(a => new { a.Id, a.CustomerId });

            addressEntity
                .HasOne(ad => ad.District)
                .WithOne()
                .HasForeignKey<MktAddress>(ad => new { ad.DistrictId, ad.AmphurId });

            modelBuilder
               .Entity<Province>()
               .ToTable("Province")
               .HasKey(p => p.Id);

            modelBuilder
                .Entity<Amphur>()
                .ToTable("Amphur")
                .HasKey(a => new { a.Id, a.ProvinceId });

            modelBuilder
                .Entity<Amphur>()
                .ToTable("Amphur")
                .HasOne(a => a.Province)
                .WithMany(p => p.Amphurs)
                .HasForeignKey(a => new { a.ProvinceId });

            modelBuilder
                .Entity<District>()
                .ToTable("District")
                .HasKey(d => new { d.Id, d.AmphurId });

            modelBuilder
                .Entity<District>()
                .ToTable("District")
                .HasOne(d => d.Amphur)
                .WithMany(a => a.Districts)
                .HasPrincipalKey(a => a.Id);

            #endregion <<< MKT Customer >>>

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

            modelBuilder
                .Entity<GroupLevel>()
                .ToTable("GroupLevel");

            modelBuilder
                .Entity<Parameter>()
                .ToTable("Parameter");
        }
    }
}
