using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RightTest.UsersBL.Commands.Registration;
using RightTest.UsersDAL.Extensions;

namespace RightTest.UsersBL.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddMediatorSource(this IServiceCollection services, IConfigurationSection section, string connectionString)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(RegistrationCommand).Assembly));

        services.AddInfrastructure(section, connectionString);
    }
}
