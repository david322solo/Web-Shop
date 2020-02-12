using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication7.Models;
using WebApplication7.Repository;

namespace WebApplication7
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options => //CookieAuthenticationOptions
                {
                   options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
               });
            services.AddMvc(option => { 
                option.EnableEndpointRouting = false; 
                
            });
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddControllersWithViews();
            services.AddSession();
            services.AddRazorPages();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();

            //dopavit
            //app.UseAuthorization();
            app.UseMvc(routes =>
            {
                

                routes.MapRoute(null, "",
                    new
                    {
                        controller = "Product",
                        action = "List",
                        category = (string)null,
                        page = 1
                    });
                routes.MapRoute(null, "Page{page}",
                    new
                    {
                        controller = "Product",
                        action = "List",
                        category = (string)null
                    },
                    new
                    {
                        page = @"\d+"
                    });

                routes.MapRoute(null, "{category}",
                    new
                    {
                        controller = "Product",
                        action = "List",
                        page = 1
                    });

                routes.MapRoute(null, "{category}/Page{page}",
                    new { controller = "Product", action = "List" },
                    new
                    {
                        page = @"\d+"
                    });
                 
              

                routes.MapRoute(null, "{controller}/{action}/{table?}/{id?}",
                    new
                    { 
                        controller = "Admin"
                    });
        


            });
         
        }
    }
}
