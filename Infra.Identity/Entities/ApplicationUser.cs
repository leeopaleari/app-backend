using Microsoft.AspNetCore.Identity;

namespace Infra.Data.Identity;

public class ApplicationUser : IdentityUser
{
    [PersonalData] public string FirstName { get; set; }

    [PersonalData] public string LastName { get; set; }

    [PersonalData] public string? ProfileImage { get; set; }

    [PersonalData] public string? FacebookUrl { get; set; }

    [PersonalData] public string? InstagramUrl { get; set; }

    [PersonalData] public string? YoutubeUrl { get; set; }

    [PersonalData] public string? WebUrl { get; set; }

    [PersonalData] public string HomeBase { get; set; }
}