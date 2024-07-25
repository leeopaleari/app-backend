using Application.DTOs.Auth;
using Infra.Identity.Interfaces;
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

    
}