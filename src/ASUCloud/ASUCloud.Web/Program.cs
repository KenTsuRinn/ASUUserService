using ASUCloud.Repository;
using ASUCloud.Service;
using ASUCloud.Web.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace ASUCloud.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Add support to logging with SERILOG
            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseSqlite(connectionString: builder.Configuration.GetConnectionString("DefaultConnection"))
               .Options;

            builder.Services.AddSingleton<DbContextOptions<ApplicationDbContext>>(options);
            builder.Services.AddSingleton<SecurityService>();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<UserRepository>();
            builder.Services.AddSingleton<EventBus>(EventBus.Instance.Subscribe());
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/User/Error");
            }
            app.UseStaticFiles();

            app.UseExceptionHandler();


            //Add support to logging request with SERILOG
            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
