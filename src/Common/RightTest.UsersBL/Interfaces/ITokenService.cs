namespace RightTest.UsersBL.Interfaces;

public interface ITokenService
{
    string GenerateJwtToken(string userId, string username);
}
