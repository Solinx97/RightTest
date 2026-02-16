using RightTest.UsersDAL.Entities;

namespace RightTest.UsersDAL.Interfaces;

public interface IUserRepository
{
    Task<bool> RegistrationAsync(string username, string password);

    Task<AppUser> LoginAsync(string username, string password);
}
