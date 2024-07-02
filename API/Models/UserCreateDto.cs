using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class UserCreateDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string FirstName { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password don't match!")]
    public string ConfirmPassword { get; set; }
    
    [Required]
    [MinLength(3)]
    [MaxLength(40)]
    public string LastName { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }
    
    public string? ProfileImage { get;  set; }
    public string? FacebookUrl { get;  set; }
    public string? InstagramUrl { get;  set; }
    public string? YoutubeUrl { get;  set; }
    public string? WebUrl { get;  set; }
    
    [Required]
    public string HomeBase { get;  set; }
}