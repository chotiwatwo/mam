using System;
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
//using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MamApi.Data.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using Serilog;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MamApi
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /* Redirect to https */
            //if (!_env.IsDevelopment())
            //{
            //    //services.AddMvc(opt =>
            //    //{
            //    //    opt.Filters.Add(new RequireHttpsAttribute());
            //    //});

            //    services.Configure<MvcOptions>(o =>
            //        o.Filters.Add(new RequireHttpsAttribute())
            //    );

            //}

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = _configuration["JWT:Issuer"],
                            ValidAudience = _configuration["JWT:Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(_configuration["JWT:PrivateKey"]))
                        };
                    });

            services.AddAutoMapper();

            services.AddSingleton(_configuration);

            services.AddDbContext<MamApiDb>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("MAMDb"), 
                b => b.UseRowNumberForPaging())                       
            );

            // Register MAM Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAppService, AppService>();
            services.AddScoped<IMasterService, MasterService>();
            services.AddScoped<IFileAttachmentService, FileAttachmentService>();

            // Register MAM Repository
            services.AddTransient<IAppRepository, MktApplicationRepository>();
            services.AddTransient<IMasterRepository, MasterRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IParameterRepository, ParameterRepository>();
            services.AddTransient<IAttachmentRepository, AttachmentRepository>();
            services.AddTransient<ICreditCheckingRepository, CreditCheckingRepository>();
            services.AddTransient<ILoginRepository, LoginRepository>();

            //services.AddCors();

            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("พบข้อผิดพลาด โปรดลองใหม่ภายหลัง");
                    });
                });
            }

            app.UseStaticFiles();

            //app.UseCors(cfg => {
            //    cfg.AllowAnyHeader()
            //       .AllowAnyMethod()
            //       .AllowAnyOrigin();
            //});

            app.UseAuthentication();

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
