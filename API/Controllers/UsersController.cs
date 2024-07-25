using API.Models;
using Application.DTOs.User;
using Infra.Identity.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsersController(IIdentityService authService) : ControllerBase
{
    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser(UserCreateApiDto userCreateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        string profileImagePath = null;

        if (userCreateDto.ProfileImagePath != null && userCreateDto.ProfileImagePath.Length > 0)
        {
            var fileName = Path.GetFileNameWithoutExtension(userCreateDto.ProfileImagePath.FileName);
            var extension = Path.GetExtension(userCreateDto.ProfileImagePath.FileName);
            var newFileName = $"{fileName}_{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}{extension}";
            var directoryPath = Path.Combine("wwwroot", "images");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            profileImagePath = Path.Combine("wwwroot/images", newFileName);


            using (var stream = new FileStream(profileImagePath, FileMode.Create))
            {
                await userCreateDto.ProfileImagePath.CopyToAsync(stream);
            }

            userCreateDto.ProfileImage = $"/images/{newFileName}";
        }

        var userDto = new UserCreateRequest
        {
            FirstName = userCreateDto.FirstName,
            LastName = userCreateDto.LastName,
            Email = userCreateDto.Email,
            Password = userCreateDto.Password,
            ProfileImage = profileImagePath,
            ProfileImagePath = userCreateDto.ProfileImagePath?.OpenReadStream(), // Converta IFormFile para Stream
            FacebookUrl = userCreateDto.FacebookUrl,
            InstagramUrl = userCreateDto.InstagramUrl,
            YoutubeUrl = userCreateDto.YoutubeUrl,
            WebUrl = userCreateDto.WebUrl,
            Homebase = userCreateDto.HomeBase
        };

        var result = await authService.CreateUser(userDto);

        if (!result.Success)
        {
            return BadRequest(new { Success = result.Success, errors = result.Errors });
        }

        return Created();
    }
}