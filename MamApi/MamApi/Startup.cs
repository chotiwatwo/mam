﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MamApi.Models;
using MamApi.Data;
using MamApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MamApi.Data.Repositories;
using AutoMapper;

namespace MamApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();

            services.AddDbContext<MamApiDb>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("MAMDb")));

            // Register MAM Services
            services.AddScoped<IAppService, AppService>();
            services.AddScoped<IMasterService, MasterService>();

            // Register MAM Repository
            services.AddTransient<IAppRepository, MktApplicationRepository>();
            services.AddTransient<IMasterRepository, MasterRepository>();
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseStaticFiles();

            app.UseMvc();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Apps}/{action=test}/{id?}");
            //});
        }
    }
}