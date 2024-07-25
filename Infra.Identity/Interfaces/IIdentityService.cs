using Application.DTOs.Auth;
using Application.DTOs.User;
using Infra.Data.Identity;

namespace Infra.Identity.Interfaces;

public interface IIdentityService
{
    Task<UserCreateResponse> CreateUser(UserCreateRequest userCreateDto);

    Task<UserCreateResponse> CreateGoogleUser(GoogleUserCreateRequest userCreateRequest);
    Task<UserLoginResponse> Login(LoginDto loginDto);
    
    Task<UserLoginResponse> LoginWithoutPassword(string email);

    Task<ApplicationUser> FindByEmailAsync(string email);
    Task Logout();

    // Task<UserLoginResponse> GenerateToken(string email);
}