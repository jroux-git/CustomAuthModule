using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspNetCoreAuthModule.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AuthModule.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Default");
            var currentAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            
            // DbContext
            services.AddDbContext<IdentityDbContext>(options => {
                options.UseSqlServer(connectionString, obj => {
                    obj.MigrationsAssembly(currentAssemblyName);
                });
            });

            // Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password = new PasswordOptions
                {
                    RequireDigit = false,
                    RequiredLength = 4,
                    RequiredUniqueChars = 4,
                    RequireUppercase = true
                };
                options.User = new UserOptions
                {
                    RequireUniqueEmail = true
                };
                options.SignIn = new SignInOptions
                {
                    RequireConfirmedEmail = false,
                    RequireConfirmedPhoneNumber = false
                };
                options.Lockout = new LockoutOptions
                {
                    AllowedForNewUsers = false,
                    DefaultLockoutTimeSpan = new System.TimeSpan(0,15,0),
                    MaxFailedAccessAttempts = 3
                };
            })
            .AddEntityFrameworkStores<IdentityDbContext>()
            .AddDefaultTokenProviders();

            // CORS
            services.AddCors(options=> {
                options.AddPolicy("CorsPolicy", cors=>{
                    cors.AllowAnyHeader();
                    cors.AllowAnyMethod();
                    cors.AllowAnyOrigin();
                });
            });

 services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Logins/Index";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("EditorOver18Policy", policy =>
                {
                    policy.RequireClaim("Over18Claim");//.RequireRole("Editor");
                });
            });

            // services.AddSingleton<IEmail, SmtpEmail>();
            // services.AddAuthentication().AddFacebook(options => {
            //     options.AppId = Configuration.GetSection("Facebook").GetValue<string>("AppId");
            //     options.AppSecret = Configuration.GetSection("Facebook").GetValue<string>("AppSecret");
            // })
            // .AddGoogle(options => {
            //     options.ClientId = Configuration.GetSection("Google").GetValue<string>("ClientId");
            //     options.ClientSecret = Configuration.GetSection("Google").GetValue<string>("ClientSecret");
            // });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
             //app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseMvc();
            // app.UseMvc(routes =>
            // {
            //     routes.MapRoute(
            //         "default",
            //         "{controller=Photos}/{action=Display}");
            // });
        }
    }
}
