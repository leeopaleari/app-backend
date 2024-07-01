using Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace Infra.Data.Identity;


public class SeedUserRoleInitial(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager) : ISeedUserRoleInitial
{
    public void SeedRoles()
    {
        if (!roleManager.RoleExistsAsync("User").Result)
        {
            var role = new IdentityRole();

            role.Name = "User";
            role.NormalizedName = "USER";

            var roleResult = roleManager.CreateAsync(role).Result;
        }
        
        if (!roleManager.RoleExistsAsync("Admin").Result)
        {
            var role = new IdentityRole();

            role.Name = "Admin";
            role.NormalizedName = "ADMIN";

            var roleResult = roleManager.CreateAsync(role).Result;
        }
    }

    public void SeedUsers()
    {
        if (userManager.FindByEmailAsync("usuario@localhost").Result == null)
        {
            var user = new ApplicationUser();
            user.UserName = "usuario@localhost";
            user.Email = "usuario@localhost";
            user.NormalizedEmail = "USUARIO@LOCALHOST";
            user.NormalizedUserName = "USUARIO@LOCALHOST";
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            user.SecurityStamp = Guid.NewGuid().ToString();

            var result = userManager.CreateAsync(user, "Numsey#2024").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "User").Wait();
            }
        }

        if (userManager.FindByEmailAsync("admin@localhost").Result == null)
        {
            var user = new ApplicationUser();

            user.UserName = "admin@localhost";
            user.Email = "admin@localhost";
            user.NormalizedEmail = "ADMIN@LOCALHOST";
            user.NormalizedUserName = "ADMIN@LOCALHOST";
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            user.SecurityStamp = Guid.NewGuid().ToString();

            var result = userManager.CreateAsync(user, "Numsey#2024").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}