using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NotesMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace NotesMVC.Data
{
    public class UserContext : IdentityDbContext<Client>
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Core Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Core Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        //public DbSet<Client> Clients { get; set; } = default!;

        //promqna
        /*
        
        protected void Seed(UserContext context)
        {
            if (!context.Roles.Any(r => r.Name == "admin"))
            {
                context.Roles.Add(new IdentityRole { Name = "admin" });
                context.Roles.Add(new IdentityRole { Name = "client" });
                var UserManager = new UserManager<Client>(new UserStore<Client>(context));
                var userRole = new IdentityUserRole<>();
                context.UserRoles.Add(new IdentityUserRole { RoleId = context.Roles.SingleOrDefault(m => m.Name == "admin").Id, UserId = context.Users.SingleOrDefault(m => m.Email == "admin@gmail.com").Id });
                context.SaveChanges();

            }
            
        }
        */
    }
}
