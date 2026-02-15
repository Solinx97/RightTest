namespace RightTest.UsersDAL.Interfaces;

public interface IUserRepository
{
    Task<bool> RegistrationAsync(string username, string password);

    Task<string> LoginAsync(string username, string password);
}
