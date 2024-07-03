using Application.DTOs.Auth;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    IIdentityService authService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<UserToken>> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var result = await authService.Login(loginDto);

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    // private UserToken GenerateToken(LoginDto userInfo)
    // {
    //     var claims = new[]
    //     {
    //         new Claim("email", userInfo.UserName),
    //         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    //     };
    //
    //     var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
    //
    //     // Gera assinatura digital
    //     var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
    //
    //     // Define tempo de expiração
    //     var expiration = DateTime.UtcNow.AddMinutes(10);
    //
    //     // Gera o token
    //     JwtSecurityToken token = new JwtSecurityToken(
    //         issuer: configuration["Jwt:Issuer"],
    //         audience: configuration["Jwt:Audience"],
    //         claims: claims,
    //         expires: expiration,
    //         signingCredentials: credentials
    //     );
    //
    //     return new UserToken()
    //     {
    //         Token = new JwtSecurityTokenHandler().WriteToken(token),
    //         Expiration = expiration
    //     };
    // }
}