using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Yetenek_Pusulasi_Web_Platform.Data;
using Yetenek_Pusulasi_Web_Platform.Services.Interfaces;
using Yetenek_Pusulasi_Web_Platform.Services.Interfaces.Factories;

namespace Yetenek_Pusulasi_Web_Platform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddControllersWithViews();
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();

            // Servislerimizi kaydediyoruz
            builder.Services.AddScoped<IScenarioService, ScenarioService>();

            // Factory ve bağımlı olduğu içerik üreteçlerini kaydet
            builder.Services.AddTransient<ProblemSolvingContentGenerator>();
            builder.Services.AddTransient<EmpathyContentGenerator>();
            builder.Services.AddScoped<IScenarioFactory, ScenarioFactory>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // MVC için varsayılan route
            app.MapControllerRoute(
               name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
