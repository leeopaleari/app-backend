using System.Text.Json.Serialization;

namespace API.Models;

public class UserToken
{
    [JsonPropertyName("access_token")]
    public string? Token { get; set; }
    public DateTime Expiration { get; set; }
}