using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Surf.Data;
using Microsoft.AspNetCore.Identity;
using Surf.Areas.Identity.Data;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Http.Headers;

namespace Surf
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //// Opret en CultureInfo med punkt som decimal separator
            //CultureInfo cultureInfo = new CultureInfo("en-US");
            //cultureInfo.NumberFormat.NumberDecimalDigits = 2;
            //cultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
            //// Anvend den nye kultur til din applikation
            //System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            //Thread.CurrentThread.CurrentUICulture = cultureInfo; 


            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext< SurfDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SurfDbContext") ?? throw new InvalidOperationException("Connection string 'SurfDbContext' not found.")));


                        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<SurfDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            builder.Services.AddHttpClient("API", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7054/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // Add other configurations as needed

            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                await SeedData.InitializeAsync(services);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();;

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}