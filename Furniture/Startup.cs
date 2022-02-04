using Furniture.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Furniture
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)//dependency injection container
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)//Uygulama ak���n� konfig�re etmek i�in environment de�erlerini set edece�imiz yer, lokal development i�in Properties\launchSettings.json dosyas�d�r.
        {
            if (env.IsDevelopment())//pipeline as madeup middlewares ,last middleware back to server
            {
                app.UseDeveloperExceptionPage();//E�er ASPNETCORE_ENVIRONMENT global variable �n de�er �Development� ise UseDeveloperExceptionPage middleware inin, request ak��� s�ras�nda ilk olarak devreye al�nmas�. Dolay�s� ile, uygulama aya�a kalkt�ktan sonra request �ncelikle buradan ge�ecek ve yine en son bu middleware den ge�ip broweser a do�ru yola ��kacak.
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
