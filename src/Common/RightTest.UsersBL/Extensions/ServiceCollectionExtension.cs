using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RightTest.UsersBL.Commands.Registration;
using RightTest.UsersBL.Interfaces;
using RightTest.UsersBL.Options;
using RightTest.UsersBL.Services;
using RightTest.UsersDAL.Extensions;

namespace RightTest.UsersBL.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddMediatorSource(this IServiceCollection services, IConfigurationSection section, string connectionString)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(RegistrationCommand).Assembly));

        services.Configure<JWTOptions>(section);

        services.AddScoped<ITokenService, TokenService>();

        services.AddUsersInfrastructure(connectionString);
    }
}
