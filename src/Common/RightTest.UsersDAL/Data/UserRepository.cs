using Microsoft.AspNetCore.Identity;
using RightTest.UsersDAL.Entities;
using RightTest.UsersDAL.Interfaces;

namespace RightTest.UsersDAL.Data;

internal class UserRepository(UserManager<AppUser> userManager) : IUserRepository
{
    private readonly UserManager<AppUser> _userManager = userManager;

    public async Task<bool> RegistrationAsync(string username, string password)
    {
        var user = new AppUser
        {
            UserName = username,
        };

        var result = await _userManager.CreateAsync(user, password);

        return result.Succeeded;
    }

    public async Task<AppUser> LoginAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
        {
            throw new Exception("User not found.");
        }

        return user;
    }
}
