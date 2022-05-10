using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using AM.Infrastructure.IoC;
using BM.Infrastructure.IoC;
using CM.Infrastructure.Configure;
using DM.Infrastructure.Configure;
using Framework.Application;
using Framework.Application.Email;
using Framework.Application.Sms;
using Framework.Infrastructure;
using IM.Infrastructure.Configure;
using IM.Presentation.Api;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Query.Contracts.Order;
using Query.Query;
using SM.Infrastructure.Configure;
using SM.Presentation.Api;

namespace ServiceHost
{
    public class Startup
    {
        #region inj

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region services

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("LampShade");

            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));
            services.AddHttpContextAccessor();

            #region configure bootstrapes

            ShopBootstrapper.Configure(services, connectionString);
            DiscountBootstrapper.Configure(services, connectionString);
            InventoryBootstrapper.Configure(services, connectionString);
            BlogBootstrapper.Configure(services, connectionString);
            CommentBootstrapper.Configure(services, connectionString);
            AccountBootstrapper.Configure(services, connectionString);

            services.AddTransient<ICartQuery, CartQuery>();

            #endregion

            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IFileUploader, FileUploader>();
            services.AddTransient<IAuthHelper, AuthHelper>();
            services.AddTransient<ISmsService, SmsService>();
            services.AddTransient<IEmailService, EmailService>();



            #region authentication

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = new PathString("/Account");
                    o.LogoutPath = new PathString("/Account");
                    o.AccessDeniedPath = new PathString("/AccessDenied");
                });

            #endregion

            #region authorization

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminArea",
                    builder => builder.RequireRole(new List<string> {Roles.Administrator, Roles.ContentUploader}));
                options.AddPolicy("AccountFolder",
                    builder => builder.RequireRole(new List<string> {Roles.Administrator}));
                options.AddPolicy("DiscountFolder",
                    builder => builder.RequireRole(new List<string> {Roles.Administrator}));
                options.AddPolicy("ShopFolder", builder => builder.RequireRole(new List<string> {Roles.Administrator}));
            });

            services.AddRazorPages()
                .AddMvcOptions(option => option.Filters.Add<SecurityPageFilter>())
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
                    options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "AccountFolder");
                    options.Conventions.AuthorizeAreaFolder("Administration", "/Discount", "DiscountFolder");
                    options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "ShopFolder");
                })
                .AddApplicationPart(typeof(ProductsController).Assembly)
                .AddApplicationPart(typeof(InventoryController).Assembly)
                .AddNewtonsoftJson();

            #endregion

            services.AddControllers();
        }

        #endregion

        #region configure

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });
        }

        #endregion
    }
}