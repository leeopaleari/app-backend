using Application.DTOs.Auth;
using Application.DTOs.User;

namespace Application.Interfaces;

public interface IIdentityService
{
    Task<UserCreateResponse> CreateUser(UserCreateRequest userCreateDto);
    Task<UserLoginResponse> Login(LoginDto loginDto);
    
    Task Logout();
}