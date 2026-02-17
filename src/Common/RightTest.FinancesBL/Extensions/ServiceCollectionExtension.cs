using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RightTest.FinancesBL.Commands.CreateCurrency;
using RightTest.FinancesBL.Interfaces;
using RightTest.FinancesBL.Options;
using RightTest.FinancesBL.Services;
using RightTest.FinancesDAL.Extensions;

namespace RightTest.FinancesBL.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddMediatorSource(this IServiceCollection services, IConfigurationSection section, string connectionString)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateCurrencyCommand).Assembly));

        services.AddFinancesInfrastructure(connectionString);

        services.Configure<ServersOptions>(section);

        services.AddHttpClient<IExternalService, ExternalService>();
    }
}
