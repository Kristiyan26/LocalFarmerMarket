using Microsoft.EntityFrameworkCore;
using LocalFarmerMarket.Data;
using LocalFarmerMarket.Core.Models;
using LocalFarmerMarket.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using LocalFarmerMarket.Services;


namespace LocalFarmerMarket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();


            builder.Services.AddScoped<IPasswordHasher<Customer>, PasswordHasher<Customer>>();
            builder.Services.AddHttpClient<ApiClient>();

            builder.Services.AddAuthorization();

            var app = builder.Build();

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
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
