using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.DTOs.Auth;
using Application.DTOs.User;
using Infra.Data.Identity;
using Infra.Identity.Configurations;
using Infra.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Infra.Identity.Services;

public class IdentityService(
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    IOptions<JwtOptions> jwtOptions)
    : IIdentityService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<UserCreateResponse> CreateUser(UserCreateRequest userCreateDto)
    {
        var applicationUser = new ApplicationUser
        {
            FirstName = userCreateDto.FirstName,
            LastName = userCreateDto.LastName,
            UserName = userCreateDto.Email,
            Email = userCreateDto.Email,
            FacebookUrl = userCreateDto.FacebookUrl,
            WebUrl = userCreateDto.WebUrl,
            InstagramUrl = userCreateDto.InstagramUrl,
            YoutubeUrl = userCreateDto.YoutubeUrl,
            HomeBase = userCreateDto.Homebase,
            ProfileImage = userCreateDto.ProfileImage,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(applicationUser, userCreateDto.Password);

        if (result.Succeeded)
            // Implementar este trecho quando houver confirmação de email
            await userManager.SetLockoutEnabledAsync(applicationUser, false);

        var userRegisteredResponse = new UserCreateResponse(result.Succeeded);

        if (!result.Succeeded && result.Errors.Count() > 0)
            userRegisteredResponse.AddErrors(result.Errors.Select(r => r.Description));

        return userRegisteredResponse;
    }

    public async Task<UserCreateResponse> CreateGoogleUser(GoogleUserCreateRequest userCreateRequest)
    {
        var applicationUser = new ApplicationUser
        {
            FirstName = userCreateRequest.FirstName,
            LastName = userCreateRequest.LastName,
            UserName = userCreateRequest.Email,
            Email = userCreateRequest.Email,
            FacebookUrl = string.Empty,
            WebUrl = string.Empty,
            InstagramUrl = string.Empty,
            YoutubeUrl = string.Empty,
            HomeBase = userCreateRequest.Homebase,
            ProfileImage = userCreateRequest.ProfileImage,
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(applicationUser);

        if (result.Succeeded)
            // Implementar este trecho quando houver confirmação de email
            await userManager.SetLockoutEnabledAsync(applicationUser, false);

        var userRegisteredResponse = new UserCreateResponse(result.Succeeded);

        if (!result.Succeeded && result.Errors.Count() > 0)
            userRegisteredResponse.AddErrors(result.Errors.Select(r => r.Description));

        return userRegisteredResponse;
    }

    public async Task<UserLoginResponse> Login(LoginDto loginDto)
    {
        var result = await signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, true);
        
        var userLoginResponse = new UserLoginResponse(result.Succeeded);

        switch (result.Succeeded)
        {
            case true:
                return await GenerateToken(loginDto.UserName);
            case false when result.IsLockedOut:
                userLoginResponse.AddErrors("This account has been blocked");
                break;
            case false when result.IsNotAllowed:
                userLoginResponse.AddErrors("Access not allowed");
                break;
            case false when result.RequiresTwoFactor:
                userLoginResponse.AddErrors("Two factor authentication is required");
                break;
            case false:
                userLoginResponse.AddErrors("Username or password are incorrect.");
                break;
        }

        return userLoginResponse;
    }

    public async Task<UserLoginResponse> LoginWithoutPassword(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        
        var userLoginResponse = new UserLoginResponse();
        
        if (await userManager.IsLockedOutAsync(user))
            userLoginResponse.AddErrors("Essa conta está bloqueada");
        else if (!await userManager.IsEmailConfirmedAsync(user))
            userLoginResponse.AddErrors("Essa conta precisa confirmar seu e-mail antes de realizar o login");

        if (userLoginResponse.Success)
            return await GenerateToken(user.Email);

        return userLoginResponse;
    }

    public async Task<ApplicationUser> FindByEmailAsync(string email)
    {
        var result = await userManager.FindByEmailAsync(email);
        return result;
    }


    public async Task Logout()
    {
        await signInManager.SignOutAsync();
    }

    private async Task<UserLoginResponse> GenerateToken(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        var tokenClaims = await GetClaims(user);

        var expirationDate = DateTime.Now.AddSeconds(_jwtOptions.AccessTokenExpiration);

        // Gera o token
        var jwt = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: tokenClaims,
            notBefore: DateTime.Now,
            expires: expirationDate,
            signingCredentials: _jwtOptions.SigningCredentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new UserLoginResponse(
            success: true,
            token: token,
            expirationDate: expirationDate);
    }

    private async Task<IList<Claim>> GetClaims(ApplicationUser user)
    {
        var claims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email!));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

        foreach (var role in roles)
            claims.Add(new Claim("role", role));

        return claims;
    }
}