using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Models;
using Application.DTOs;
using Domain.Account;
using Infra.Data.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    IAuthenticate authService,
    IConfiguration configuration) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
    {
        try
        {
            var result = await authService.Authenticate(userInfo.UserName, userInfo.Password);

            if (result)
            {
                return GenerateToken(userInfo);
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return BadRequest(ModelState);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateDto registerDto)
    {
        var result = await authService.RegisterUser(
            firstName: registerDto.FirstName,
            lastName: registerDto.LastName,
            profileImage: registerDto.ProfileImage,
            password: registerDto.Password,
            email: registerDto.Email,
            facebookUrl: registerDto.FacebookUrl,
            webUrl: registerDto.WebUrl,
            instagramUrl: registerDto.InstagramUrl,
            youtubeUrl: registerDto.YoutubeUrl,
            homeBase: registerDto.HomeBase);
    
        if (result)
        {
            return Ok();
        }
    
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return BadRequest(ModelState);
    }

    private UserToken GenerateToken(LoginModel userInfo)
    {
        var claims = new[]
        {
            new Claim("email", userInfo.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));

        // Gera assinatura digital
        var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

        // Define tempo de expiração
        var expiration = DateTime.UtcNow.AddMinutes(10);

        // Gera o token
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}