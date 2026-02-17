using Microsoft.AspNetCore.Identity;

namespace RightTest.UsersDAL.Entities;

public class AppUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;
}
