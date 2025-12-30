using Microsoft.EntityFrameworkCore;

namespace ProniaA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<DAL.AppDbContext>
            (
                options =>
                {
                    options.UseSqlServer("Server=localhost;Database=PironiaDB;Trusted_Connection=true;Encrypt=false");
                }
            );



            var app = builder.Build();
            app.UseStaticFiles();

            app.MapControllerRoute
            (
                name: "admin",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );


            app.MapControllerRoute
            (
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );


            app.Run();
        }
    }
}
