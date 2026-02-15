using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RightTest.UsersDAL.Entities;

namespace RightTest.UsersDAL.Persistent;

public class AppUsersContext(DbContextOptions<AppUsersContext> options) : IdentityDbContext<AppUser>(options)
{
}
