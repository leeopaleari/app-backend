using Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace Infra.Data.Identity;

public class AuthenticateService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager) : IAuthenticate
{
    public async Task<bool> Authenticate(string email, string password)
    {
        var result = await signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);

        return result.Succeeded;
    }

    public async Task<bool> RegisterUser(string email,
        string password,
        string firstName,
        string lastName,
        string instagramUrl,
        string facebookUrl,
        string webUrl,
        string youtubeUrl,
        string profileImage,
        string homebase)
    {
        var applicationUser = new ApplicationUser
        {
            FirstName = firstName,
            LastName = lastName,
            UserName = email,
            Email = email,
            FacebookUrl = facebookUrl,
            WebUrl = webUrl,
            InstagramUrl = instagramUrl,
            YoutubeUrl = youtubeUrl,
            HomeBase = homebase
        };
    
        var result = await userManager.CreateAsync(applicationUser, password);
    
        if (result.Succeeded)
        {
            await signInManager.SignInAsync(applicationUser, false);
        }
    
        return result.Succeeded;
    }

    public async Task Logout()
    {
        await signInManager.SignOutAsync();
    }
}