using System.Security.Claims;
using Application.DTOs.User;
using Infra.Identity.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OAuthController(
    IIdentityService authService) : ControllerBase
{
    [HttpGet]
    [Route("authorize")]
    public IActionResult GoogleLogin()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = Url.Action("GoogleResponse")
        };

        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet]
    [Route("callback")]
    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        if (!result.Succeeded)
            return BadRequest();

        // var info = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
        //
        // if (info == null)
        //     return BadRequest();

        var email = result.Principal.FindFirstValue(ClaimTypes.Email);

        var user = await authService.FindByEmailAsync(email);
        if (user == null)
        {
            var newUser = new GoogleUserCreateRequest()
            {
                Email = email,
                UserName = email,
                FirstName = result.Principal.FindFirstValue(ClaimTypes.GivenName),
                LastName = result.Principal.FindFirstValue(ClaimTypes.Surname),
                Homebase = result.Principal.FindFirstValue(ClaimTypes.Locality) ?? "",
                ProfileImage = result.Principal.FindFirstValue("picture"),
            };

            var createUserResult = await authService.CreateGoogleUser(newUser);

            if (!createUserResult.Success)
                return BadRequest(createUserResult.Errors);
        }

        var tokenResponse = await authService.LoginWithoutPassword(email);
        return Ok(tokenResponse);
    }
}