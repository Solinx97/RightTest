using Microsoft.EntityFrameworkCore;
using RightTest.FinancesDAL.Entities;
using RightTest.FinancesDAL.Persistent;
using Testcontainers.PostgreSql;

namespace RightTest.FinancesBL.IntegrationTests.Data;

public class PostgresqlFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container;

    public PostgresqlFixture()
    {
        _container = new PostgreSqlBuilder("postgres:16-alpine")
          .WithDatabase("Currency")
          .WithUsername("postgres")
          .WithPassword("124124")
          .WithCleanUp(true)
          .Build();
    }

    public DbContextOptions<FinancesContext> Options { get; private set; } = null!;

    public string Connection { get; private set; } = string.Empty;

    public FinancesContext CreateContext()
    {
        return new FinancesContext(Options);
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        Connection = _container.GetConnectionString();
        Options = new DbContextOptionsBuilder<FinancesContext>()
            .UseNpgsql(Connection)
            .Options;
    }

    public static async Task SeedCurrencyTestDataAsync(FinancesContext context, Guid id)
    {
        await context.Set<Currency>().AddAsync(
            new Currency(id, "BYN", 2.11m)
        );

        await context.SaveChangesAsync();
    }

    public static async Task SeedCurrencyTestDataAsync(FinancesContext context, int length, bool isSameName = false, bool isSameRate = false)
    {
        for (int i = 0; i < length; i++)
        {
            await context.Set<Currency>().AddAsync(
                new Currency(Guid.NewGuid(), isSameName ? "BYN" : "BYN" + i, isSameRate ? 2.11m : 2.11m + i + 1)
            );
        }

        await context.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await _container.StopAsync();
    }
}
