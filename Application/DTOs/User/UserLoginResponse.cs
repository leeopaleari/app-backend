using System.Text.Json.Serialization;

namespace Application.DTOs.User;

public class UserLoginResponse
{
    public bool Success { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Token { get; private set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? ExpirationDate { get; set; }

    public List<string> Errors { get; set; }

    public UserLoginResponse() => Errors = new List<string>();

    public UserLoginResponse(bool success = true) : this() => Success = success;

    public UserLoginResponse(bool success, string token, DateTime expirationDate) : this(success)
    {
        Token = token;
        ExpirationDate = expirationDate;
    }

    public void AddErrors(string error) => Errors.Add(error);
}