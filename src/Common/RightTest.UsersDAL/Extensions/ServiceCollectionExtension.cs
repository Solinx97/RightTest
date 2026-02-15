using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RightTest.UsersDAL.Consts;
using RightTest.UsersDAL.Data;
using RightTest.UsersDAL.Entities;
using RightTest.UsersDAL.Interfaces;
using RightTest.UsersDAL.Persistent;

namespace RightTest.UsersDAL.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddUsersInfrastructure(this IServiceCollection services, IConfigurationSection section, string connectionString)
    {
        services.AddDbContext<AppUsersContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddIdentityCore<AppUser>()
            .AddEntityFrameworkStores<AppUsersContext>();

        services.Configure<JWT>(section);

        services.AddScoped<IUserRepository, UserRepository>();
    }

    public static void AddUsersInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppUsersContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddIdentityCore<AppUser>()
            .AddEntityFrameworkStores<AppUsersContext>();
    }
}
