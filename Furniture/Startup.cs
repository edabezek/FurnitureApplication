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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)//Uygulama akýþýný konfigüre etmek için environment deðerlerini set edeceðimiz yer, lokal development için Properties\launchSettings.json dosyasýdýr.
        {
            if (env.IsDevelopment())//pipeline as madeup middlewares ,last middleware back to server
            {
                app.UseDeveloperExceptionPage();//Eðer ASPNETCORE_ENVIRONMENT global variable ýn deðer “Development” ise UseDeveloperExceptionPage middleware inin, request akýþý sýrasýnda ilk olarak devreye alýnmasý. Dolayýsý ile, uygulama ayaða kalktýktan sonra request öncelikle buradan geçecek ve yine en son bu middleware den geçip broweser a doðru yola çýkacak.
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
