using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bobaadmin.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;




namespace bobaadmin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services )
        {

            services.AddDbContext<bobachaContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("BobaConnection")));
            services.Configure<CookiePolicyOptions>(options =>
            {
            options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddScoped<BobaDA>();

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizePage("/Contact");
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            #region snippet1
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            // Call UseAuthentication before calling UseMVC.
            #region snippet2
            app.UseAuthentication();
            #endregion
            app.UseMvc(routes =>
            {
                routes
                //.MapRoute(name: "Ajax", template: "Ajax/{Controller}/{action}")
                
                .MapRoute(name: "Sushi", template: "/menu/sushi", defaults: new { controller = "Menu", action = "Sushi" })
                .MapRoute(name: "Milktea", template: "/menu/milktea", defaults: new { controller = "Menu", action = "Milktea" })
                .MapRoute(name: "Menu", template: "menu", defaults: new { controller = "Menu", action = "Index" })
                .MapRoute(name: "Order", template: "order", defaults: new { controller = "Order", action = "Index" })
                .MapRoute(name: "OrderView", template: "order/{stt}", defaults: new { controller = "Order", action = "ViewCate" })
                .MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }

    }
}
