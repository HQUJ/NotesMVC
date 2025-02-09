using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotesMVC.Data;
using Microsoft.AspNetCore.Identity;
using NotesMVC.Models;
namespace NotesMVC
{
    public class Program
    {
        //promqna da e Task a ne void
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<NotesMVCContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("NotesMVCContext") ?? throw new InvalidOperationException("Connection string 'NotesMVCContext' not found.")));

            //builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            //promqna
            builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NotesMVCContext")));

            builder.Services.AddDefaultIdentity<Client>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UserContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            //promqna
            app.MapRazorPages();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            //promqna
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "admin", "client" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Client>>();

                string email = "admin@admin.com";
                string password = "AdminPassword1234$";

                if(await userManager.FindByEmailAsync(email)==null)
                {
                    var user = new Client();
                    user.UserName = email;
                    user.Email = email;
                    user.EmailConfirmed = true;
                    user.FirstName = "Administrator";

                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, "admin");

                }
            }
            //kraj na pormqnta


            app.Run();
        }
    }
}
