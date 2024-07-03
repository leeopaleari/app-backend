namespace Application.DTOs.User;

public class UserCreateResponse
{
    public bool Success { get; private set; }

    public List<string> Errors { get; private set; }

    public UserCreateResponse() => Errors = new List<string>();

    public UserCreateResponse(bool sucess = true) : this() => Success = sucess;

    public void AddErrors(IEnumerable<string> errors) => Errors.AddRange(errors);
}