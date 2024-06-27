namespace Domain.Entities;

public sealed class User : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string? ProfileImage { get; private set; }
    public string? Facebook { get; private set; }
    public string? Instagram { get; private set; }
    public string? YouTube { get; private set; }
    public string? Web { get; private set; }
    public string HomeBase { get; private set; }
}