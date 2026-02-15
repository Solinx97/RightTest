using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RightTest.FinancesDAL.Persistent;

namespace RightTest.FinancesDAL.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddFinancesInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<FinancesContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
    }
}
