using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Migrations.Commands.ApplyFinancesMigrations;
using RightTest.FinancesDAL.Extensions;
using RightTest.UsersDAL.Extensions;

namespace Migrations.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddMediatorSource(this IServiceCollection services, IConfiguration config)
    {
        var usersConnection = config.GetConnectionString("UsersConnection") ?? string.Empty;
        var financesConnection = config.GetConnectionString("FinancesConnection") ?? string.Empty;

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ApplyFinancesMigrationsCommand).Assembly));

        services.AddUsersInfrastructure(usersConnection);
        services.AddFinancesInfrastructure(financesConnection);
    }
}
