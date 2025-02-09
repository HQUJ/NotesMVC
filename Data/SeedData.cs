using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using NotesMVC.Models;

namespace NotesMVC.Data
{
    /*
    public class SeedData
    {
        
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new UserContext(
            serviceProvider.GetRequiredService<
                    DbContextOptions<UserContext>>()))
            {
                context.Roles.Add(new IdentityRole { Name = "admin" });
                context.SaveChanges();
                var role = context.Roles.SingleOrDefault(m => m.Name == "admin");
                var UserManager = new UserManager<Client>( context.Users.SingleOrDefault(m => m.Email == "admin@gmail.com"));
                var account = new AccountControle
                user.Roles.Add(new IdentityUserRole { RoleId = role.Id });
            }
        }
       
    }
    */
}
