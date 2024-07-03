using System.Text.Json.Serialization;

namespace Application.DTOs.Auth;

public class UserToken
{
    [JsonPropertyName("access_token")]
    public string? Token { get; set; }
    public DateTime Expiration { get; set; }
}