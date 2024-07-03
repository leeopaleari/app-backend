using Application.DTOs.Auth;
using Application.DTOs.User;
using Infra.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infra.Identity.Interfaces;
public interface IAuthenticate
{
    Task<bool> Authenticate(string email, string password);

    Task<IdentityResult> RegisterUser(UserCreateRequest request);

    Task Logout();

    Task<ApplicationUser> GetLoggedUser();

    UserToken GenerateToken(LoginDto loginDto);
}