using Microsoft.Extensions.DependencyInjection;
using RightTest.FinancesBL.Commands.CreateCurrency;
using RightTest.FinancesDAL.Extensions;

namespace RightTest.FinancesBL.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddMediatorSource(this IServiceCollection services, string connectionString)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateCurrencyCommand).Assembly));

        services.AddFinancesInfrastructure(connectionString);
    }
}
