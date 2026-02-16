using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RightTest.UsersDAL.Data;
using RightTest.UsersDAL.Entities;
using RightTest.UsersDAL.Interfaces;
using RightTest.UsersDAL.Persistent;

namespace RightTest.UsersDAL.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddUsersInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppUsersContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddIdentityCore<AppUser>()
            .AddEntityFrameworkStores<AppUsersContext>();

        services.AddScoped<IUserRepository, UserRepository>();
    }
}
