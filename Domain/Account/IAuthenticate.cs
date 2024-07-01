namespace Domain.Account;

public interface IAuthenticate
{
    Task<bool> Authenticate(string email, string password);

    Task<bool> RegisterUser(string email,
        string password,
        string firstName,
        string lastName,
        string instagramUrl,
        string facebookUrl,
        string webUrl,
        string youtubeUrl,
        string homeBase,
        string profileImage);

    Task Logout();
}