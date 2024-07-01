using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth;

public class RegisterDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string FirstName { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    [MinLength(3)]
    [MaxLength(40)]
    public string LastName { get; set; }

    public string Email { get; set; }
    public string? ProfileImage { get;  set; }
    public string? FacebookUrl { get;  set; }
    public string? InstagramUrl { get;  set; }
    public string? YouTubeUrl { get;  set; }
    public string? WebUrl { get;  set; }
    
    [Required]
    public string HomeBase { get;  set; }
}