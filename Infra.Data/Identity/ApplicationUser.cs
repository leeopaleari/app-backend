using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Infra.Data.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get;  set; }
    
    public string LastName { get;  set; }
    public string? ProfileImage { get;  set; }
    public string? FacebookUrl { get;  set; }
    public string? InstagramUrl { get;  set; }
    public string? YouTubeUrl { get;  set; }
    public string? WebUrl { get;  set; }
    public string HomeBase { get;  set; }
}