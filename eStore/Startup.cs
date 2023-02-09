using eStore.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore
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
            services.AddControllersWithViews();

            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromMinutes(100);
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
            });

            services.AddAutoMapper(typeof(MappingConfig));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpClient<IProductService, ProductService>();
            //services.AddScoped<IProductService, ProductService>();
            services.AddSingleton<IProductService, ProductService>();

            services.AddHttpClient<IOrderDetailService, OrderDetailService>();
            services.AddSingleton<IOrderDetailService, OrderDetailService>();

            services.AddHttpClient<IOrderService, OrderService>();
            services.AddSingleton<IOrderService, OrderService>();

            services.AddHttpClient<IMemberService, MemberService>();
            services.AddSingleton<IMemberService, MemberService>();

            services.AddHttpClient<IAuthService, AuthService>();
            services.AddSingleton<IAuthService, AuthService>();            
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "Manage",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
