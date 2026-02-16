using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RightTest.UsersBL.Interfaces;
using RightTest.UsersBL.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RightTest.UsersBL.Services;

internal class TokenService(IOptions<JWTOptions> jwtOptions) : ITokenService
{
    private readonly JWTOptions _jwtOptions = jwtOptions.Value;

    public string GenerateJwtToken(string userId, string username)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Email, username),
            new Claim("scopes", _jwtOptions.Scopes)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audiences,
            claims: claims,
            expires: DateTime.Now.AddHours(_jwtOptions.ValidHours),
            signingCredentials: creds
            );

        var result = new JwtSecurityTokenHandler().WriteToken(token);

        return result;
    }
}
