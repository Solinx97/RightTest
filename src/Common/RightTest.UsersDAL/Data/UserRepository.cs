using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RightTest.UsersDAL.Consts;
using RightTest.UsersDAL.Entities;
using RightTest.UsersDAL.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RightTest.UsersDAL.Data;

internal class UserRepository(UserManager<AppUser> userManager, IOptions<JWT> jwt) : IUserRepository
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly JWT _jwt = jwt.Value;

    public async Task<bool> RegistrationAsync(string username, string password)
    {
        var user = new AppUser
        {
            UserName = username,
        };

        var result = await _userManager.CreateAsync(user, password);

        return result.Succeeded;
    }

    public async Task<string> LoginAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
        {
            throw new Exception("User not found.");
        }

        var token = GenerateJwtToken(user);
        return token;
    }

    private string GenerateJwtToken(AppUser user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds);

        var result = new JwtSecurityTokenHandler().WriteToken(token);

        return result;
    }
}
