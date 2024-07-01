using Application.DTOs.Auth;
using Domain.Account;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    IAuthenticate authenticate) : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (ModelState.IsValid)
        {
            var result = await authenticate.RegisterUser(
                firstName: registerDto.FirstName,
                lastName: registerDto.LastName,
                profileImage: registerDto.ProfileImage,
                password: registerDto.Password,
                email: registerDto.Email,
                facebookUrl: registerDto.FacebookUrl,
                webUrl: registerDto.WebUrl,
                instagramUrl: registerDto.InstagramUrl,
                youtubeUrl: registerDto.YouTubeUrl,
                homeBase:registerDto.HomeBase);
        }

        return Ok();
    }
}